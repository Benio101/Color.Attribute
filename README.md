# <img align="right" src="https://raw.githubusercontent.com/Benio101/Color.Attribute/master/Color.Attribute/Logo.ico"> Color.Attribute
[Visual Studio](https://visualstudio.microsoft.com) extension: Color C++ Attributes.

## Status
| Branch | Build Status
| ---    | ---
| [`master`](https://github.com/Benio101/Color.Attribute/tree/master) | [![Build status](https://ci.appveyor.com/api/projects/status/af3p32abt7un5ul3/branch/master?svg=true)](https://ci.appveyor.com/project/Benio101/color-attribute/branch/master)
| [`2017`](https://github.com/Benio101/Color.Attribute/tree/2017) | [![Build status](https://ci.appveyor.com/api/projects/status/af3p32abt7un5ul3/branch/2017?svg=true)](https://ci.appveyor.com/project/Benio101/color-attribute/branch/2017)

## Description
Extension adds options to overwrite colors of certain C++ Attributes.<br>
Extension works in files of `ContentType` `"C/C++"`, _eg_ `.cpp` or `.h` files.

## Usage
New entries will appear in `Tools` → `Options` → `Environment` → `Fonts and Colors` → `Text Editor`.<br>
Each will begin with `C++ Attribute:` prefix.
- Edit their color values, until you want to keep extension default ones (listed below).
- If you don't want to change some attribute's color at all, set it's `Item foreground` value to `Automatic`.

![](https://raw.githubusercontent.com/Benio101/Color.Attribute/master/Color.Attribute/Preview.png)

## Preview
| Default Attributes without extension | Color Attributes with extension enabled
| --- | ---
| ![](https://raw.githubusercontent.com/Benio101/Color.Attribute/master/Color.Attribute/PreviewDisabled.png) | ![](https://raw.githubusercontent.com/Benio101/Color.Attribute/master/Color.Attribute/PreviewEnabled.png)

## Limitations
In order to provide fast execution, some rare syntax patterns are not supported by extension right now.
There are no plans to waive those restrictions until intellisense shall classify C++ attributes.

### Nested `[[` & `]]`
Due to high cost of parsing [`balanced-token`](http://eel.is/c++draft/dcl.attr.grammar#nt:balanced-token), extension non greedily matches anything between `[[` and `]]` in [`SnapshotSpan`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.snapshotspan?view=visualstudiosdk-2017), splits content by `,` and trims, then finally matches standard attributes. Thus, attribute containing nested double squares can be incorrectly colored, _eg_ `[[test::covered([[likely]])]]`.

### Comments
Comments in attributes are not supported. They are treated as text for faster execution. This can cause some attributes to be incorrectly colored, _eg_ `[[using/*civ1*/civ2: fast, nodiscard]]`.

### Multiline attributes
If multiline attribute will be split into multiple spans upon classification, it won't be matched at all.

### `deprecated` reason
[`deprecated`](https://en.cppreference.com/w/cpp/language/attributes/deprecated) attribute's reason, if present, must be enclosed in `"`, and such created `string-literal` can be prefixed by any number of characters different than `"`, _eg_ `[[deprecated(u8"Use API v2 instead.")]]`, but not `[[deprecated(CMacro_Reason)]]`.

### Contract expression
[Contract](https://en.cppreference.com/w/cpp/language/attributes/contract)'s expression can not contain `]]`. Parsing expression could be too complex and expensive for this simple extension, _eg_ `[[ensures axiom R: Internal::TextDB::EntryExists("[[" + R + "]]")]]`.

## List of Attributes
Extension exposes all [Standard Attributes](https://en.cppreference.com/w/cpp/language/attributes#Standard_attributes) for customization.

Full list of customizable Attribute entries, with their default colors:

| Attribute entry                 | Type      | Color      | RGB (0 – 255) |
| :---                            | :---      | :---       | :---          |
| _C++ Attribute_                 | Plain     | Dark White | 176, 176, 176 |
| `carries_dependency`            | Keyword   | Blue       | 128, 176, 224 |
| `deprecated`                    | Important | Dark Red   | 224, ​ 96, ​ 96 |
| `deprecated`: Reason            | String    | Dark Green | 128, 176, ​ 96 |
| `fallthrough`                   | Flow      | Dark Blue  | ​ 96, 128, 224 |
| `likely`                        | Positive  | Green      | 176, 224, 128 |
| `maybe_unused`                  | Positive  | Green      | 176, 224, 128 |
| `no_unique_address`             | Keyword   | Blue       | 128, 176, 224 |
| `nodiscard`                     | Warning   | Yellow     | 224, 224, 128 |
| `noreturn`                      | Important | Dark Red   | 224, ​ 96, ​ 96 |
| `optimize_for_synchronized`     | Keyword   | Blue       | 128, 176, 224 |
| `unlikely`                      | Negative  | Red        | 224, 128, 128 |
| Contract: `assert`              | Warning   | Yellow     | 224, 224, 128 |
| Contract: `ensures`             | Positive  | Green      | 176, 224, 128 |
| Contract: `ensures`: Identifier | Plain     | Dark White | 176, 176, 176 |
| Contract: `expects`             | Param     | Orange     | 224, 176, 128 |
| Contract: Expression            | Plain     | Dark White | 176, 176, 176 |
| Contract: Level: `audit`        | Warning   | Yellow     | 224, 224, 128 |
| Contract: Level: `axiom`        | Positive  | Green      | 176, 224, 128 |
| Contract: Level: `default`      | Negative  | Red        | 224, 128, 128 |
| Punctuation                     | Punct     | Gray       | 128, 128, 128 |
| Punctuation: `[[` & `]]`        | Punct     | Gray       | 128, 128, 128 |
