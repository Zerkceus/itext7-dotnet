/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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

namespace iText.Layout.Minmaxwidth {
    public class MinMaxWidth {
        private float childrenMinWidth;

        private float childrenMaxWidth;

        private float additionalWidth;

        private float availableWidth;

        public MinMaxWidth(float additionalWidth)
            : this(0, 0, additionalWidth) {
        }

        [System.ObsoleteAttribute(@"Will be removed in 7.1. Use MinMaxWidth(float) instead.")]
        public MinMaxWidth(float additionalWidth, float availableWidth)
            : this(additionalWidth, availableWidth, 0, 0) {
        }

        public MinMaxWidth(float childrenMinWidth, float childrenMaxWidth, float additionalWidth)
            : this(additionalWidth, MinMaxWidthUtils.GetMax(), childrenMinWidth, childrenMaxWidth) {
        }

        [System.ObsoleteAttribute(@"Will be removed in 7.1. Use MinMaxWidth(float, float, float) instead.")]
        public MinMaxWidth(float additionalWidth, float availableWidth, float childrenMinWidth, float childrenMaxWidth
            ) {
            this.childrenMinWidth = childrenMinWidth;
            this.childrenMaxWidth = childrenMaxWidth;
            this.additionalWidth = additionalWidth;
            this.availableWidth = availableWidth;
        }

        public virtual float GetChildrenMinWidth() {
            return childrenMinWidth;
        }

        public virtual void SetChildrenMinWidth(float childrenMinWidth) {
            this.childrenMinWidth = childrenMinWidth;
        }

        public virtual float GetChildrenMaxWidth() {
            return childrenMaxWidth;
        }

        public virtual void SetChildrenMaxWidth(float childrenMaxWidth) {
            this.childrenMaxWidth = childrenMaxWidth;
        }

        public virtual float GetAdditionalWidth() {
            return additionalWidth;
        }

        [System.ObsoleteAttribute(@"Will be removed in 7.1. Available width should be always equal to MinMaxWidthUtils.GetMax()"
            )]
        public virtual float GetAvailableWidth() {
            return availableWidth;
        }

        public virtual void SetAdditionalWidth(float additionalWidth) {
            this.additionalWidth = additionalWidth;
        }

        public virtual float GetMaxWidth() {
            return Math.Min(childrenMaxWidth + additionalWidth, availableWidth);
        }

        public virtual float GetMinWidth() {
            return Math.Min(childrenMinWidth + additionalWidth, GetMaxWidth());
        }

        public override String ToString() {
            return "min=" + (childrenMinWidth + additionalWidth) + ", max=" + (childrenMaxWidth + additionalWidth) + "; ("
                 + availableWidth + ")";
        }
    }
}
