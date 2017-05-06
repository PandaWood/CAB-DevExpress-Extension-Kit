CABDevExpress.ExtensionKit 4th Drop

### Release Notes

#### Things changed
- I've gone back to calling release 'drops' because they are not as official as a 'version' as it's a
source code framework and somtimes we're literally just dropping stuff in - and never delivering binaries.

- Removed IUIBarItemLinksAdapter (and implemented BarLinksOwnerCollectionUIAdapter : BarLinksCollectionUIAdapter
removed IRibbonGroupUI (wasn't used)

- Lots of small code clean-up in the Adapters (using Guard.ArgumentNotNull consistently; not checking for null if
it's unnecessary, or doing if it is; wrong class name used in XML comments etc)

* Removed the InternalCollection property for most UIElementAdapters (most of them weren't used, nor should be)

* Fixed XtraNavBarWorkspace.Close() bug where NavBarGroups were not removed

#### Things Added

- Ribbon support Added
- RibbonApplicationMenuUIAdapter
- RibbonPageHeaderUIAdapter
- RibbonQuickAccessToolbarUIAdapter
- RibbonGalleryUIAdapter and RibbonGalleryGroupUIAdapter (thanks to DevLynx)

Tests have been written and included using XUnit (not very comprehensive as yet, but it's a start)
see the XUnit CodePlex site at http://www.codeplex.com/xunit 

Note: there is a "ReSharper" plugin for XUnit at http://www.codeplex.com/xunitext/Release/ProjectReleases.aspx


We appreciate any comments or contributions.

------------------------------------------------------------------------------
###### Contributors:

- Espen Schaathun espen_schaathun@hotmail.com
- Vincent Guerci puy0@hotmail.com
- Peter van der Woude (PandaWood) pandawoude@gmail.com
- DevLynx http://DevLynx.LiveJournal.com
