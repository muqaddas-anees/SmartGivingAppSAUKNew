/// Tweak to fix sluggish behavior in IE6/7 for the ASP.NET Futures (May 2007) release. 
/// Instead of computing the bounds of the drop element everytime, it is computed first 
/// and stored as a property of the element. 

Sys.Preview.UI.IEDragDropManager.prototype._findPotentialTarget = function(dragSource, dragVisual) {
        var ev = window._event;

        if (!this._dropTargets) {
            return null;
        }

        var type = dragSource.get_dragDataType();
        var mode = dragSource.get_dragMode();
        var data = dragSource.getDragData(this._activeContext);

        var scrollOffset = this.getScrollOffset(document.body,  true);
        var x = ev.clientX + scrollOffset.x; 
        var y = ev.clientY + scrollOffset.y;
        var cursorRect = { x: x - this._radius, y: y - this._radius, width: this._radius * 2, height: this._radius * 2 };
        
        for (var i = 0; i < this._dropTargets.length; i++) {
            var dt = this._dropTargets[i];
            var canDrop = dt.canDrop(mode, type, data);
            if(!canDrop) continue;

            var el = dt.get_dropTargetElement(); 
            
            //store as expando
            var targetRect;
            if (el.___bounds) {
                targetRect = el.___bounds;
            }
            else {
                targetRect = Sys.UI.DomElement.getBounds(el);
                el.___bounds= targetRect;
            }
            
            var overlaps = Sys.UI.Control.overlaps(cursorRect, targetRect);
            
            if (overlaps || el === document.body) {
                return dt;
            }
        }        
        return null;
    }