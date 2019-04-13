# LazZiya.TagHelpers

Collection of helpful TagHelpers for any ASP.NET Core project. Latest version 2.0.0 contains the below TagHelpers :

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
Install-Package LazZiya.TagHelpers -Version 2.0.0
````

add tag helper to _ViewImports.cshtml:

````razor
@addTagHelper *, LazZiya.TagHelpers
````

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
