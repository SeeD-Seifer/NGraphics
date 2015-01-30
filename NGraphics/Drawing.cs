using System;
using System.Collections.Generic;
using System.Linq;

namespace NGraphics
{
	public delegate void DrawingFunc (ICanvas surface);

	public class Drawing
	{
		readonly Size size;
		readonly DrawingFunc func;

		Graphic graphic = null;

		public Graphic Graphic {
			get {
				if (graphic == null) {
					try {
						DrawGraphic ();
					} catch (Exception ex) {
						graphic = new Graphic (size);
						Log.Error (ex);
					}
				}
				return graphic;
			}
		}

		public Drawing (Size size, DrawingFunc func)
		{
			if (func == null) {
				throw new ArgumentNullException ("func");
			}
			this.size = size;
			this.func = func;
		}

		public void Invalidate ()
		{
			graphic = null;
		}

		void DrawGraphic ()
		{
			var c = new GraphicCanvas (size);
			if (func != null)
				func (c);
			graphic = c.Graphic;
		}
	}
}
