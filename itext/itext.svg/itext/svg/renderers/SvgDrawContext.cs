/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2018 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS

    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/

    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.

    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.

    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.

    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com
 */
using System;
using System.Collections.Generic;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Svg.Exceptions;

namespace iText.Svg.Renderers {
    /// <summary>
    /// The SvgDrawContext keeps a stack of
    /// <see cref="iText.Kernel.Pdf.Canvas.PdfCanvas"/>
    /// instances, which
    /// represent all levels of XObjects that are added to the root canvas.
    /// </summary>
    public class SvgDrawContext {
        private readonly IDictionary<String, Object> namedObjects = new Dictionary<String, Object>();

        private readonly Stack<PdfCanvas> canvases = new Stack<PdfCanvas>();

        private readonly Stack<Rectangle> viewports = new Stack<Rectangle>();

        /// <summary>Retrieves the current top of the stack, without modifying the stack.</summary>
        /// <returns>the current canvas that can be used for drawing operations.</returns>
        public virtual PdfCanvas GetCurrentCanvas() {
            return canvases.Peek();
        }

        /// <summary>
        /// Retrieves the current top of the stack, thereby taking the current item
        /// off the stack.
        /// </summary>
        /// <returns>the current canvas that can be used for drawing operations.</returns>
        public virtual PdfCanvas PopCanvas() {
            return canvases.Pop();
        }

        /// <summary>
        /// Adds a
        /// <see cref="iText.Kernel.Pdf.Canvas.PdfCanvas"/>
        /// to the stack (by definition its top), for use in
        /// drawing operations.
        /// </summary>
        /// <param name="canvas">the new top of the stack</param>
        public virtual void PushCanvas(PdfCanvas canvas) {
            canvases.Push(canvas);
        }

        /// <summary>
        /// Get the current size of the stack, signifying the nesting level of the
        /// XObjects.
        /// </summary>
        /// <returns>the current size of the stack.</returns>
        public virtual int Size() {
            return canvases.Count;
        }

        /// <summary>Adds a viewbox to the context.</summary>
        /// <param name="viewPort">rectangle representing the current viewbox</param>
        public virtual void AddViewPort(Rectangle viewPort) {
            this.viewports.Push(viewPort);
        }

        /// <summary>Get the current viewbox.</summary>
        /// <returns>the viewbox as it is currently set</returns>
        public virtual Rectangle GetCurrentViewPort() {
            return this.viewports.Peek();
        }

        /// <summary>Remove the currently set view box.</summary>
        public virtual void RemoveCurrentViewPort() {
            if (this.viewports.Count > 0) {
                this.viewports.Pop();
            }
        }

        /// <summary>Adds a named object to the draw context.</summary>
        /// <remarks>Adds a named object to the draw context. These objects can then be referenced from a different tag.
        ///     </remarks>
        /// <param name="name">name of the object</param>
        /// <param name="namedObject">object to be referenced</param>
        public virtual void AddNamedObject(String name, Object namedObject) {
            if (namedObject == null) {
                throw new SvgProcessingException(SvgLogMessageConstant.NAMED_OBJECT_NULL);
            }
            if (name == null || String.IsNullOrEmpty(name)) {
                throw new SvgProcessingException(SvgLogMessageConstant.NAMED_OBJECT_NAME_NULL_OR_EMPTY);
            }
            if (!this.namedObjects.ContainsKey(name) || namedObject is PdfFormXObject) {
                this.namedObjects.Put(name, namedObject);
            }
        }

        /// <summary>Get a named object based on its name.</summary>
        /// <remarks>Get a named object based on its name. If the name isn't listed, this method will return null.</remarks>
        /// <param name="name">name of the object you want to reference</param>
        /// <returns>the referenced object</returns>
        public virtual Object GetNamedObject(String name) {
            return this.namedObjects.Get(name);
        }
    }
}
