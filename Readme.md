<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication1/Form1.vb))
<!-- default file list end -->
# How to filter a lookup's data source, so that it provides unique values


<p>This example is a version of the <a href="https://www.devexpress.com/Support/Center/p/A237">How to filter a second LookUp column based on a first LookUp column's value</a> KB Article. Here, the products lookup is filtered so that it won't be possible to have records with the same product in the grid control. To achieve this, a negative IN operator is used in the DataView.RowFilter expression.<br />
To see how the example works, run it and try to set empty products. You will see that the lookup is filtered. To clear the editor's value, use the standard Control+Delete keystroke.</p><p><strong>See Also:</strong><br />
<a href="http://www.csharp-examples.net/dataview-rowfilter">http://www.csharp-examples.net/dataview-rowfilter</a>/</p>

<br/>


