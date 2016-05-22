// thanks to http://www.timvw.be/2008/08/02/presenting-the-sortablebindinglistt-take-two/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

public class PropertyComparer<T> : IComparer<T>
{
    #region Private fields

    readonly IComparer comparer;
    PropertyDescriptor propertyDescriptor;
    int reverse;

    #endregion

    #region Constructors

    public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
    {
        propertyDescriptor = property;
        Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);
        comparer = (IComparer)comparerForPropertyType.InvokeMember("Default",
            BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null);

        SetListSortDirection(direction);
    }

    #endregion

    #region IComparer<T> Members

    public int Compare(T x, T y)
    {
        return reverse * comparer.Compare(propertyDescriptor.GetValue(x), propertyDescriptor.GetValue(y));
    }

    #endregion

    #region Descriptors and properties

    void SetPropertyDescriptor(PropertyDescriptor descriptor)
    {
        propertyDescriptor = descriptor;
    }

    void SetListSortDirection(ListSortDirection direction)
    {
        reverse = direction == ListSortDirection.Ascending ? 1 : -1;
    }

    public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
    {
        SetPropertyDescriptor(descriptor);
        SetListSortDirection(direction);
    }

    #endregion
}