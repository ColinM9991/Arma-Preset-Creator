# Arma Preset Creator

## Description

This is a tool that I wrote some years back during my time playing Arma 3. My biggest pet peeve has always been the lack of support for converting Steam Workshop collections into presets that can be used within Bohemia Interactive's Arma 3 Launcher.

This is hosted [here](http://www.armapresetcreator.com/)

## Dependencies

.NET Core 5 - https://dotnet.microsoft.com/download/dotnet/5.0
The Hosting Bundle is recommended as this will conditionally install all required components, for if you decide to use IIS to host this.

## Setup

1. Clone the repository
2. Register for a [Steam Web API key](https://partner.steamgames.com/doc/webapi_overview/auth)
3. Update the configuration file to include your Steam Web API Key.

## Operations

There are two API endpoints available in this application. The first allows you to retrieve the dependencies of a Workshop item. This includes all top level and child nodes within the tree structure that is a Workshop item.

Currently this will send duplicate requests to Steam, for instance, where CBA_A3 is listed as a dependency for a top level and child node/Workshop item. This code was written over the course of 2~3 hours (with an Angular application for the frontend) and once I'd gotten it working I never booted the solution back up again.


### Retrieve Steam Workshop Item Details
```
GET /api/steam/workshop/publisheditems/{workshopItemId}
```

The second operation is what renders the view, or HTML, which contains the Workshop items. THis is compatible with the Arma 3 Launcher. In the past I used an Angular app which saved the HTML as a file.

### Retrieve Preset file with mods provided by the Workshop Item Details operation
```
POST /api/arma/preset/generate

...PAYLOD THAT WAS RETRIEVED FROM /api/steam/workshop/publisheditems/{workshopItemId}...
```
