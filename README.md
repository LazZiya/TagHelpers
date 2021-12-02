# LazZiya.TagHelpers

## What is it?
A collection of useful TagHelpers for any ASP.NET Core project.

## Documentation :
See all documentation in [DOCS.Ziyad.info][1].

### [Paging TagHelper][1]
Create a pagination control _styled with bootstrap 4.x_ using simple html tag.

````html
<paging page-no="Model.PageNo" 
        page-size="Model.PageSize"
        total-records="Model.TotalRecords">
</paging>
````
[![PagingTagHelper default](https://github.com/LazZiya/WebXRObjects/raw/master/Shared/Images/LazZiya.TagHelpers/paging-tag-helper-full.PNG)][1]

### [Alert TagHelper ][1]
Create bootstrap alerts using very simple html tag.

#### Front end alert
````html
<alert-success>
    My alert text ...
</alert>
````

#### Backend alert
````cs
TempData.Success("My alert text ...")
````

Catch all backend alerts in frontend:
````html
<alert></alert>
````

[![AlertTagHelper - success](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/alert-taghelper-success.PNG)][1]

### [Language Navigation TagHelper][1]
Create a language dropdown navigation for websites. Supported cultures will be used to create the navigation items.

````html
<language-nav flags="true"></language-nav>
````
[![LanguageNavTagHelper with flags](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/languagenav-taghelper-with-flags.PNG)][1]

### [Localization Validation Scripts TagHelper][1]
Add all client side scripts that are required for validating localized inputs like decimal numbers, dates, ..etc.
````html
<localization-validation-scripts></localization-validation-scripts>
````
[![Localization number es](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/localization-validiation-scripts-number-es.PNG)][1]

## Live demos:
http://demo.ziyad.info/en/

[1]:https://docs.ziyad.info
