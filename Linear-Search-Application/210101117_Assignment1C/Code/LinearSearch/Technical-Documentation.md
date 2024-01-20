# Technical Documentation of Linear Search Application in VB.NET
## By Dhanesh V, 210101117

## Form1 Class

### Private Fields

1. **filePath (Private String):** Stores the path of the file to be processed.
2. **targetElement (Private String):** Stores the target element to be searched in the ListView.
3. **considerCase (Private Boolean):** A flag indicating whether the search should be case-sensitive.
4. **speedVal (Private Integer):** Specifies the animation speed during item highlighting in milliseconds.

### Methods

#### `NormalizeFloat(input As String) As String`

- **Description:** Normalizes the input string by attempting to parse it as a double and converting it back to a string.
- **Parameters:**
  - `input (String):` The input string to be normalized.
- **Returns:**
  - `String:` The normalized string.

#### `Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load`

- **Description:** Handles the form load event and initializes default settings.
- **Initialization:**
  - `considerCase:` Defaulted to `False`.
  - `speedVal:` Defaulted to `500`.
  - Adds animation speed options to ComboBox and sets the default selection to "Medium."

#### `LoadFileContent(filePath As String)`

- **Description:** Reads the content of the specified file and populates the ListView.
- **Parameters:**
  - `filePath (String):` The path of the file to be loaded.
- **File Reading:**
  - Uses a StreamReader to read the file line by line.
  - Normalizes float values using the `NormalizeFloat` method.
  - Populates the ListView with ListViewItem objects.

#### `ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click`

- **Description:** Handles the button click event to initiate the search operation.
- **Validation:**
  - Checks if the file is empty and displays an error message if true.
  - Retrieves the target element from the TextBox and validates if it is empty.

#### `wait(interval As Integer)`

- **Description:** Waits for the specified interval using a custom wait mechanism.
- **Parameters:**
  - `interval (Integer):` The interval to wait in milliseconds.

#### `HighlightItems()`

- **Description:** Highlights items in the ListView based on the specified target element.
- **Search Algorithm:**
  - Iterates through ListView items.
  - Compares items with the target, considering case sensitivity.
  - Highlights items with animation and displays messages when the target is found or not.

#### `HighlightItem(index As Integer, color As Color)`

- **Description:** Highlights a specific item in the ListView with the specified color.
- **Parameters:**
  - `index (Integer):` The index of the item to be highlighted.
  - `color (Color):` The color to use for highlighting.

#### `OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk`

- **Description:** Handles the file selection dialog event and loads file content when the dialog is closed.
- **File Loading:**
  - Calls `LoadFileContent` if a file is selected.

#### `ButtonLoad_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLoad.Click`

- **Description:** Handles the button click event to open the file selection dialog.
- **Dialog Opening:**
  - Initiates the OpenFileDialog for file selection.

#### `RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged`

- **Description:** Handles the RadioButton checked change event to update the case sensitivity setting.
- **Update Setting:**
  - Sets `considerCase` based on the RadioButton state.

#### `ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged`

- **Description:** Handles the ComboBox selected index change event to update the animation speed setting.
- **Update Setting:**
  - Sets `speedVal` based on the selected ComboBox option.

## Note
For more explanation regarding logics of each method, refer to inline documentation in the source code.
