# Unity Editor Directories

## Supported Unity versions

Unity 2020.3 or higher.  

## Installation

Via Package Manager.  
Open the Package Manager window in your Unity editor, select `Add package from git URL...` from the `+` button in the top left, enter following and press the `Add` button.  

```text
https://github.com/neuneu9/unity-editor-directories.git?path=Packages/jp.neuneu9.editor-directories
```

Or open the `Packages/manifest.json` file and add an item like the following to the `dependencies` field.  

```json
"jp.neuneu9.editor-directories": "https://github.com/neuneu9/unity-editor-directories.git?path=Packages/jp.neuneu9.editor-directories",
```
