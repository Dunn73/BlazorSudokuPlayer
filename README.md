# Changes
## HTML
* Added script to html to handle a function that moves the caret position to the end of the element: MoveCaretToEnd
## Blazor
* Added an async Task function which calls the JSRuntime MoveCaretToEnd asynchronously, targeting the associated cell or text field.
  * Called this function immediately after the calling the FocusAsync() upon completion of a Key Down event to set the caret of the new focus.
* Added an event callback to the input handler: @onkeydown:preventDefault="@isPreventKey".
  * Created the associated bool variable isPreventKey = false in the @code{} section.
  * Set isPreventKey = true at the beginning of each of the custom event key handlers to prevent both custom and default handlers from firing simultaneously.
  * Set isPreventKey = false at the end of Async Task HandleKeyDown to return key handlers to default functionality to handle non custom handlers properly.
* Added Tab key to custom key handlers with the functionality of tabbing through each of cells.
