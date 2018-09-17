# TagHelpers
Pagination, email and phone number tag helpers for ASP.NET Core 2.1 Razor projects

### Installation:

Install via nuget :

````
Install-Package LazZiya.TagHelpers -Version 1.0.2
````

add tag helper to _ViewImports.cshtml:

````
@addTagHelper *, LazZiya.TagHelpers
````

### Create a pagination control

Only few parameters are required to fireup the agination control

````
<paging total-records="Model.TotalRecords"
            page-no="Model.PageNo"
            show-prev-next="true">
</paging>
````

or maximize the power of your pagination control with more options:
````
    <paging total-records="Model.TotalRecords"
            page-no="Model.PageNo"
            page-size="Model.PageSize"
            show-prev-next="true"
            show-total-pages="true"
            show-total-records="true"
            show-page-size-nav="true"
            show-first-numbered-page="true"
            show-last-numbered-page="true"
            gap-size="2"
            max-displayed-pages="3"
            query-string-key-page-no="p"
            query-string-key-page-size="s"
            query-string-value="@@(Request.QueryString.Value)"
            page-size-nav-block-size="10"
            page-size-nav-max-items="3"
            page-size-nav-on-change="get"
            page-size-nav-form-method="this.form.submit();"
            class="row"
            class-paging-control-div="col"
            class-info-div="col"
            class-page-size-div="col"
            class-paging-control="pagination"
            class-active-page="disabled"
            class-disabled-jumping-button="disabled"
            class-total-pages="badge badge-secondary"
            class-total-records="badge badge-info"
            text-page-size="Items per page:"
            text-total-pages="pages"
            text-total-records="records"
            text-first="&laquo;"
            text-last="&raquo;"
            text-previous="&lsaquo;"
            text-next="&rsaquo;"
            sr-text-first="First"
            sr-text-last="Last"
            sr-text-previous="Previous"
            sr-text-next="Next">
    </paging>
````


### Json Friendly
Keep your html code clean by moving all optional settings to appSettings.json, and create different instances for different use cases:

appSettings.json:
````
{
    "lazziya": {
        "pagingTagHelper": {
            "basic": {
            "show-first-last": true,
            "max-displayed-pages": 7,
            "class-active-aage": "disabled"
          },
          "custom": {
            "show-first-last": true,
            "show-prev-next": true,
            "max-displayed-pages": 2,
            "text-first": "go first",
            "text-last": "go last",
            "text-previous": "one step back",
            "text-next": "one step forward",
            "class-paging-control": "pagination pagination-lg"
          },
          "full": {
            "max-displayed-pages": 10,
            "gap-size": 3,
            "show-first-last": true,
            "show-prev-next": true,
            "show-page-size-nav": true,
            "show-total-pages": true,
            "show-total-records": true,
            "show-first-numbered-page": true,
            "show-last-numbered-page": true
          }
        }
    }
}
````
HTML Code:
````
    <!-- custom settings -->
    <paging total-records="Model.TotalRecords" 
            page-no="Model.PageNo" 
            page-size="Model.PageSize" 
            settings-json="custom">
    </paging>

    <!-- basic settings -->
    <paging total-records="Model.TotalRecords" 
            page-no="Model.PageNo" 
            page-size="Model.PageSize" 
            settings-json="basic">
    </paging>

    <!-- full settings -->
    <paging total-records="Model.TotalRecords" 
            page-no="Model.PageNo" 
            page-size="Model.PageSize" 
            settings-json="full">
    </paging>
````

### Project site:
http://www.ziyad.info/en/articles/21-Paging_TagHelper_for_ASP_NET_Core

### Live demo:
http://demo.ziyad.info/en/paging

### License
https://github.com/LazZiya/TagHelpers/blob/master/LICENSE
