# LazZiya.TagHelpers
Collection of helpful TagHelpers for any ASP.NET Core project. 
> - preview version for Asp.Net Core 3.0 is available [here][1]

## Latest release

02 September 2019
- v3.0.0-preview1
  DotNetCore 3.0 support
- [Release history](https://github.com/LazZiya/TagHelpers/blob/master/ReleseHistory.md)

## Contents
- LocalizeTagHelper ([Repository](https://github.com/lazziya/TagHelpers.Localize), [Demo](http://demo.ziyad.info/en/Localize), [Tutorial](http://www.ziyad.info/en/articles/36-Develop_Multi_Cultural_Web_Application_Using_ExpressLocalization))
- AlertTagHelper ([Docs](http://www.ziyad.info/en/articles/37-Alert_TagHelpers), [Demo](http://demo.ziyad.info/en/Alerts))
- LocalizationValidationScriptsTagHelper ([Docs](http://www.ziyad.info/en/articles/34-Localization_Validation_Scripts), [Demo](http://demo.ziyad.info/en/Trips))
- LanguageNavTagHelper ([Docs](http://www.ziyad.info/en/articles/32-Language_Navigation_TagHelper), [Demo](http://demo.ziyad.info/en/LanguageNav))
- SelectEnumTagHelper ([Docs](http://www.ziyad.info/en/articles/28-Select_Enum_TagHelper), [Demo](http://demo.ziyad.info/en/SelectEnum))
- PagingTagHelper ([Docs](http://www.ziyad.info/en/articles/21-Paging_TagHelper_for_ASP_NET_Core), [Demo](http://demo.ziyad.info/en/Paging), [Tutorial](http://www.ziyad.info/en/articles/38-How_to_build_an_efficient_pagination_system))
- EmailTagHelper
- PhoneNumberTagHelper

## Installation:

Install via nuget :

````
Install-Package LazZiya.TagHelpers -Version 3.0.0-preview1
````

add tag helper to _ViewImports.cshtml:

````razor
@addTagHelper *, LazZiya.TagHelpers
````

# Code Samples
## Localize TagHelper
Use simple html tag to localize text/html in razor views
````razor
<localize>Hellow world!</localize>
````
_Requires [LazZiya.TagHelpers.Localization](https://github.com/lazziya/TagHelpers.Localize) and [LazZiya.ExpressLocalization](https://github.com/lazziya/ExpressLocalization) nuget packages._

Read more: 
- [Demo](http://demo.ziyad.info/en/Localize)
- [Repository](https://github.com/lazziya/TagHelpers.Localization)
- [LazZiya.ExpressLocalization](https://github.com/lazziya/ExpressLocalization)
- [Step-by-step tutorial to develop multi-cultural Asp.Net Core 2.2 web app](http://www.ziyad.info/en/articles/36-Develop_Multi_Cultural_Web_Application_Using_ExpressLocalization)

## Alert TagHelper
Easily create bootstrap 4.x alerts from c# backend or razor pages using html codes.
All bootstrap alerts can be created from both ends (Primary, Secondary, Success, Info, Warning, Danger, Light, Dark).

### Create alert from razor page using HTML
````razor
<alert-success>Congratulations! you have done the job!</alert-success>
````

### Create alert from c# backend
Alert are TempData items, so they will be disposed once they are fetched.
You can create alerts from the c# backend by the provided extension methods for TempData as below:
````cs
using LazZiya.TagHelpers.Alerts

TempData.Danger("Ooopps! something went wrong with the code, please contact support.");
````

Then use alert tag helepr on razor side to render the alerts:
````razor
<alert view-context="ViewContext"></alert>
````
Read more : 
- [Docs](http://ziyad.info/en/articles/37-Alert_TagHelper)
- [Demo](http://demo.ziyad.info/en/Alerts)


## LocalizationValidationScripts TagHelper
will add all required js files and code to validate localized input fields like numbers, date and currency. These scripts will help to validate localized decimal numbers with comma or dot format (e.g. EN culture: 1.2 - TR culture: 1,2).

 1- Register tag helper component in startup. Don't apply this step if you are using [ExpressLocalization](https://github.com/LazZiya/ExpressLocalization) it will be done automatically
 ````cs
 services.AddTransient<ITagHelperComponent, LocalizationValidationScriptsTagHelperComponent>()
 ````
 
 2- Add this code to the scripts section in the page:
 ````cshtml
 <localization-validation-scripts></localization-validation-scripts>
 ````
 For more details :
 - [Docs](http://www.ziyad.info/en/articles/34-Client_Side_Localization_Validation_Scripts)
 - [Demo](http://demo.ziyad.info/en/Trips)


## LangaugeNav TagHelper
````cshtml
<language-nav view-context="ViewContext"></language-nav>
````
For more details :
- [Docs](http://www.ziyad.info/en/articles/32-Language_Navigation_TagHelper)
- [Demo](http://demo.ziyad.info/en/LanguageNav)


## SelectEnum TagHelper

Sample enum :
````cs
public enum WeekDays { MON, TUE, WED, THU, FRI, SAT, SUN }
````

create the related select list dropdown in razor page :
````razor
<select-enum 
        enum-type="typeof(WeekDays)" 
        name="weekDay">
</select-enum>
````
For more details :
- [Docs](http://www.ziyad.info/en/articles/28-Select_Enum_TagHelper)
- [Demo](http://demo.ziyad.info/en/SelectEnum)


## Paging TagHelper

Only few parameters are required to fireup the agination control

````razor
<paging total-records="Model.TotalRecords"
        page-no="Model.PageNo"
        query-string-value="@(Request.QueryString.Value)"
        show-prev-next="true">
</paging>
````

it is important to add `query-string-value` if there is multiple filtering parameters in the url.

For more details :
- [Docs](http://www.ziyad.info/en/articles/21-Paging_TagHelper_for_ASP_NET_Core)
- [Demo](http://demo.ziyad.info/en/Paging)
- [Step-by-step tutorial to build an efficient pagination system](http://www.ziyad.info/en/articles/38-How_to_build_an_efficient_pagination_system)


## Project site:
http://ziyad.info/en/articles/27-LazZiya_TagHelpers

## Live Demos :
http://demo.ziyad.info/en/

## License
https://github.com/LazZiya/TagHelpers/blob/master/LICENSE

[1]: https://github.com/LazZiya/TagHelpers/tree/TagHelpersCore3
