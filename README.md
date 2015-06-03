SelectorAttribute
=================
This package will allow you to generate an editor for simple values that represent multiple choice lists on your view models

Installation
============
Available on NuGet as the SelectorAttribute package
https://nuget.org/packages/SelectorAttribute/
```
Install-Package SelectorAttribute 
```

Usage
============
Create classes inheriting from SelectorAttributeBase to define the choices for a property of a view model.

Enum Example:
```
public class ExampleEnumSelectorAttribute : SelectorAttributeBase
{
	public ExampleSelectorAttribute()
	{
		this.BulkSelectionThreshold = 1;  // The editor will change to a dropdown when this limit is exceeded
	}

	public override IEnumerable<SelectListItem> GetItems()
	{
		// You can use the Description attribute on enum items to change the displayed text
		return SelectorHelpers.CreateItemsFromEnum<ExampleChoicesEnum>();
	}
}
```
Options Loaded from Database:
```
public class RoleSelectorAttribute : SelectorAttributeBase
    {
        public RoleSelectorAttribute()
        {
            this.BulkSelectionThreshold = 100; 
        }

        public override IEnumerable<SelectListItem> GetItems()
        {
            var options = DependencyResolver.Current.GetService<DBContext>()
                .Roles
                .OrderBy(x => x.AccessLevel)
                .ToList();

            return options.ToSelectList(m => m.Id, m => m.Name);
        }
    }
```

Annotate properties of your view model with these selectors
```
public class EditViewModel
{
	[ExampleEnumSelector]
	public ExampleEnum Example { get; set; }
	[RoleSelector]
	public int RoleId { get; set; }            
}
```

In your view, use Html.EditorFor or EditorForModel to create the selector control.

Credit
======
This code is based on code by Matt Hidinger as part of his MVC Grab bag project.
http://www.matthidinger.com/archive/2012/01/23/adding-rich-selector-support-for-mvc.aspx
http://mvcgrabbag.codeplex.com/
