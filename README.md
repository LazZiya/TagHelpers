
# LazZiya.TagHelpers

Collection of helpful TagHelpers for any ASP.NET Core project. Latest version 2.1.0 contains the below TagHelpers :

- LocalizeTagHelper : Available as a separate package in [LazZiya.TagHelpers.Localization](https://github.com/lazziya/TagHelpers.Localization)
- AlertTagHelper (new v2.2)
- LocalizationValidationScriptsTagHelper
- LanguageNavTagHelper
- PagingTagHelper
- SelectEnumTagHelper
- EmailTagHelper
- PhoneNumberTagHelper

## Project site:
http://ziyad.info/en/articles/27-LazZiya_TagHelpers

## Live Demos :
http://demo.ziyad.info/en/

## Installation:

Install via nuget :

````
Install-Package LazZiya.TagHelpers -Version 2.2.0
````

add tag helper to _ViewImports.cshtml:

````razor
@addTagHelper *, LazZiya.TagHelpers
````

## LocalizeTagHelper
Use simple html tag to localize text/html in razor views
````razor
<localize>Hellow world!</localize>
````
For more details see: [LazZiya.TagHelpers.Localization](https://github.com/lazziya/TagHelpers.Localization)

## AlertTagHelper
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
read more about alerts here: http://ziyad.info/en/articles/37-Alert_TagHelper
Live demo: http://demo.ziyad.info/en/Alerts


## Localization validation scripts
will add all required js files and code to validate localized input fields like numbers, date and currency. These scripts will help to validate localized decimal numbers with comma or dot format (e.g. EN culture: 1.2 - TR culture: 1,2).

### How to add localization validation scripts

 1- Register tag helper component in startup. Don't apply this step if you are using [ExpressLocalization](https://github.com/LazZiya/ExpressLocalization) it will be done automatically
 ````cs
 services.AddTransient<ITagHelperComponent, LocalizationValidationScriptsTagHelperComponent>()
 ````
 
 2- Add this code to the scripts section in the page:
 ````cshtml
 <localization-validation-scripts></localization-validation-scripts>
 ````
 For more details visit http://www.ziyad.info/en/articles/34-Client_Side_Localization_Validation_Scripts 


### How to create a langauge navigation dropdown list
````cshtml
<language-nav view-context="ViewContext"></language-nav>
````
For more details visit http://www.ziyad.info/en/articles/32-Language_Navigation_TagHelper 


### How to create a select list dropdown from enum

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
For more details visit http://www.ziyad.info/en/articles/28-Select_Enum_TagHelper


### How to create a pagination control

Only few parameters are required to fireup the agination control

````razor
<paging total-records="Model.TotalRecords"
        page-no="Model.PageNo"
        query-string-value="@(Request.QueryString.Value)"
        show-prev-next="true">
</paging>
````

it is important to add `query-string-value` if there is multiple filtering parameters in the url.

For more details visit http://www.ziyad.info/en/articles/21-Paging_TagHelper_for_ASP_NET_Core

see more in WiKi pages or project site.

goto Wiki: https://github.com/LazZiya/TagHelpers/wiki

goto project website: http://ziyad.info/en/articles/27-LazZiya_TagHelpers

## License
https://github.com/LazZiya/TagHelpers/blob/master/LICENSE
