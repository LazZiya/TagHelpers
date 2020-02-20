# LazZiya.TagHelpers

## What is it?
A collection of useful TagHelpers for any ASP.NET Core project.

## Documentation :
See all documentation in [wiki space][https://github.com/LazZiya/TagHelpers/wiki].

### [Paging TagHelper][1]
Create a pagination control _styled with bootstrap 4.x_ using simple html tag.

````html
<paging page-no="Model.PageNo" 
        page-size="Model.PageSize"
        total-records="Model.TotalRecords">
</paging>
````
[![PagingTagHelper default](https://github.com/LazZiya/WebXRObjects/raw/master/Shared/Images/LazZiya.TagHelpers/paging-tag-helper-full.PNG)][1]

### [Alert TagHelper ][2]
Create bootstrap alerts using very simple html tag.

````html
<alert-success>
    My alert text ...
</alert>
````
[![AlertTagHelper - success](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/alert-taghelper-success.PNG)][2]

### [Language Navigation TagHelper][3]
Create a language dropdown navigation for websites. Supported cultures will be used to create the navigation items.

````html
<language-nav flags="true"></language-nav>
````
[![LanguageNavTagHelper with flags](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/languagenav-taghelper-with-flags.PNG)][3]

### [Localization Validation Scripts TagHelper][4]
Add all client side scripts that are required for validating localized inputs like decimal numbers, dates, ..etc.
````html
<localization-validation-scripts></localization-validation-scripts>
````
[![Localization number es](https://github.com/LazZiya/WebXRObjects/blob/master/Shared/Images/LazZiya.TagHelpers/localization-validiation-scripts-number-es.PNG)][4]

## Live demos:
http://demo.ziyad.info/en/

[1]:https://github.com/LazZiya/TagHelpers/wiki/Paging-TagHelper-Basic-Setup
[2]:https://github.com/LazZiya/TagHelpers/wiki/Alerts-TagHelper-Front-end-Alerts
[3]:https://github.com/LazZiya/TagHelpers/wiki/LanguageNav-TagHelper-Setup
[4]:https://github.com/LazZiya/TagHelpers/wiki/LocalizationValidationScripts-TagHelper-Setup
