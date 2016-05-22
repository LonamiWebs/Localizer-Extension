# Localizer Extension
Localizer Extension is a simple Visual Studio extension that allows you to extract string resources from C# source files and from .xaml files.

This extension is intended to be used under C# WPF projects only.

## Installation
Simply download the extension and install it. After installed, the extension will become available in Visual Studio.

## Getting started
### First steps
To use this extension, simply right click on any string `string test = "right click here"` in a `.cs` file or on `Text="right click here"` in a `.xaml` file. A message box will be prompted to input a name for the resource. After clicking _accept_ the resource will be extracted to `res/Strings.xaml`. This file can be modified manually.
### Managing translations
Under the `Project` Visual Studio menu you will find a new entry called `Manage strings resources`. Click it, and a new window will appear. In this window you can:
- Add new locales (such as es-ES, en-UK, etc)
- Delete existing locales (the `String.ll-CC.xaml` resource files will be removed from disk)
- Add a custom resources file, which can include images and other files. Must be edited manually.

In this window you will also be able to open the `Settings` window which allows you to change some stuff such as how the resources folder is called or how the resources class is called.

## License
This project is licensed under the DBAD license