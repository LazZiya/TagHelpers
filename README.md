# LazZiya.TagHelpers

Helpful TagHelpers for any ASP.NET Core project. Latest version 1.0.3 contains the below TagHelpers :

- PagingTagHelper
- SelectEnumTagHelper
- EmailTagHelper
- PhoneNumberTagHelper

## Project site:
http://ziyad.info/en/articles/27-LazZiya_TagHelpers

## Live Demo :
http://demo.ziyad.info/en/SelectEnum

## Installation:

Install via nuget :

````
Install-Package LazZiya.TagHelpers -Version 1.0.3
````

add tag helper to _ViewImports.cshtml:

````
@addTagHelper *, LazZiya.TagHelpers
````


### How to create a select list dropdown from enum
````
<select-enum 
        enum-type="typeof(WeekDays)" 
        name="weekDay">
</select-enum>
````

### How to create a pagination control

Only few parameters are required to fireup the agination control

````
<paging total-records="Model.TotalRecords"
            page-no="Model.PageNo"
            show-prev-next="true">
</paging>
````

see more in WiKi pages or project site.

goto Wiki: https://github.com/LazZiya/TagHelpers/wiki

goto project website: http://ziyad.info/en/articles/27-LazZiya_TagHelpers

## License
https://github.com/LazZiya/TagHelpers/blob/master/LICENSE
