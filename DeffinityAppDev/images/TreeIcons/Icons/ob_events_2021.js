function ob_OnNodeDrop(src, dst)
{
	// alert("OnNodeDrop from " + src + " to " + dst);
}

function ob_OnNodeSelect(id)
{
	// alert("OnNodeSelect on " + id);
}

function ob_OnNodeEdit(id, text, prevText)
{
	// alert("OnNodeEdit on " + id + "\n  text: " + text + "\n prevText: " + prevText);	
}

function ob_OnAddNode(parentId, childId, textOrHTML, expanded, image, subTreeURL)
{
	/*
	alert("OnAddNode:\n  parentId: " + (parentId || "none")
			+ "\n  childId: " + (childId || "none")
			+ "\n  textOrHTML: " + (textOrHTML || "none")
			+ "\n  expanded: " + (expanded || "false")
			+ "\n  image: " + (image || "none")
			+ "\n  subTreeURL: " + (subTreeURL || "none"));
	*/
}

function ob_OnRemoveNode(id)
{
	// alert("OnRemoveNode on " + id);
}

/*
	Pre-events.
	Use them to implement your own validation for such operations as add, remove, edit
*/

function ob_OnBeforeAddNode(parentId, childId, textOrHTML, expanded, image, subTreeURL)
{
	// add your own validation code
	// e.g. it may use synchronized obout postback to query
	// server side application whether such operation is valid
	return true;
}

function ob_OnBeforeRemoveNode(id)
{
	// add your own validation code
	// e.g. it may use synchronized obout postback to query
	// server side application whether such operation is valid
	return true;
}

function ob_OnBeforeNodeEdit(id)
{
	// add your own validation code
	// e.g. it may use synchronized obout postback to query
	// server side application whether such operation is valid
	return true;
}

function ob_OnBeforeNodeDrop(src, dst)
{
	// add your own validation code
	// e.g. it may use synchronized obout postback to query
	// server side application whether such operation is valid
	return true;
}