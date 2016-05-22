using System;
using System.Collections.Generic;
using System.ComponentModel;

class SortableBindingList<T> : BindingList<T>, IBindingListView
{
    #region Private fields

    // sorting
    readonly Dictionary<Type, PropertyComparer<T>> comparers;
    bool isSorted;
    ListSortDirection listSortDirection;
    PropertyDescriptor propertyDescriptor;

    // filtering
    private List<T> originalListValue = new List<T>();
    private List<T> OriginalListValue => new List<T>(originalListValue);

    #endregion

    #region Constructors

    public SortableBindingList() : base(new List<T>())
    {
        comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    public SortableBindingList(IEnumerable<T> enumeration)
    {
        originalListValue = new List<T>(enumeration);
        foreach (var item in OriginalListValue)
            Items.Add(item);
        
        comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    #endregion

    #region Properties

    protected override bool SupportsSortingCore => true;

    protected override bool IsSortedCore => isSorted;

    protected override PropertyDescriptor SortPropertyCore => propertyDescriptor;

    protected override ListSortDirection SortDirectionCore => listSortDirection;

    protected override bool SupportsSearchingCore => true;

    public bool SupportsAdvancedSorting => false;
    public ListSortDescriptionCollection SortDescriptions => null;
    public void ApplySort(ListSortDescriptionCollection sorts)
    {
        throw new NotSupportedException();
    }

    // filtering
    public bool SupportsFiltering => true;

    public void RemoveFilter()
    {
        if (filter != null) filter = null;
    }

    private string filter = null;

    public string Filter
    {
        get { return filter; }
        set
        {
            if (filter == value) return;

            // Turn off list-changed events.
            RaiseListChangedEvents = false;

            // If the value is null or empty, reset list.
            if (string.IsNullOrEmpty(value))
            {
                ResetList();
            }
            else
            {
                ApplyFilter(value);
            }
            filter = value;
            RaiseListChangedEvents = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    #endregion

    #region Sort core

    protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    {
        List<T> itemsList = (List<T>)Items;

        Type propertyType = property.PropertyType;
        PropertyComparer<T> comparer;
        if (!comparers.TryGetValue(propertyType, out comparer))
        {
            comparer = new PropertyComparer<T>(property, direction);
            comparers.Add(propertyType, comparer);
        }

        comparer.SetPropertyAndDirection(property, direction);
        itemsList.Sort(comparer);

        propertyDescriptor = property;
        listSortDirection = direction;
        isSorted = true;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        isSorted = false;
        propertyDescriptor = base.SortPropertyCore;
        listSortDirection = base.SortDirectionCore;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override int FindCore(PropertyDescriptor property, object key)
    {
        int count = Count;
        for (int i = 0; i < count; ++i)
            if (property.GetValue(this[i]).Equals(key))
                return i;

        return -1;
    }

    public override void EndNew(int itemIndex)
    {
        if (isSorted && itemIndex > 0 && itemIndex == Count - 1)
        {
            ApplySortCore(propertyDescriptor, listSortDirection);
            base.EndNew(itemIndex);
        }
    }

    #endregion

    #region Searching

    public int Find(string property, object key)
    {
        var properties = TypeDescriptor.GetProperties(typeof(T));
        var prop = properties.Find(property, true);

        if (prop == null) return -1;
        else return FindCore(prop, key);
    }

    #endregion

    #region Filtering

    void ResetList()
    {
        ClearItems();
        foreach (T t in OriginalListValue)
            Items.Add(t);

        if (IsSortedCore)
            ApplySortCore(SortPropertyCore, SortDirectionCore);
    }

    protected override void OnListChanged(ListChangedEventArgs e)
    {
        // If the list is reset, check for a filter. If a filter 
        // is applied don't allow items to be added to the list.
        if (e.ListChangedType == ListChangedType.Reset)
        {
            if (string.IsNullOrEmpty(Filter))
                AllowNew = true;
            else
                AllowNew = false;
        }
        // Add the new item to the original list.
        if (e.ListChangedType == ListChangedType.ItemAdded)
        {
            originalListValue.Add(this[e.NewIndex]);
            if (!string.IsNullOrEmpty(Filter))
            //if (Filter == null || Filter == "")
            {
                string cachedFilter = Filter;
                Filter = string.Empty;
                Filter = cachedFilter;
            }
        }
        // Remove the new item from the original list.
        if (e.ListChangedType == ListChangedType.ItemDeleted)
        {
            if (e.NewIndex < originalListValue.Count)
                originalListValue.RemoveAt(e.NewIndex);
        }

        base.OnListChanged(e);
    }


    internal void ApplyFilter(string filter)
    {
        List<T> results = new List<T>();

        // Check each value and add to the results list.
        foreach (T item in OriginalListValue)
        {
            if (item is IComparable<string>)
            {
                if (((IComparable<string>)item).CompareTo(filter) > 0)
                    results.Add(item);
            }
            else
            {
                if (item.ToString().IndexOf(filter, StringComparison.OrdinalIgnoreCase) > -1)
                    results.Add(item);
            }
        }

        ClearItems();
        foreach (T itemFound in results)
            Add(itemFound);
    }

    #endregion
}