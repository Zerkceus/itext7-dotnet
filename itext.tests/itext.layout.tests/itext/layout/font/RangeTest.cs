using System;

namespace iText.Layout.Font {
    public class RangeTest {
        [NUnit.Framework.Test]
        public virtual void TestWrongRange() {
            NUnit.Framework.Assert.That(() =>  {
                new RangeBuilder().AddRange(11, 10);
            }
            , NUnit.Framework.Throws.TypeOf<ArgumentException>());
;
        }

        [NUnit.Framework.Test]
        public virtual void TestWrongRangeSize() {
            NUnit.Framework.Assert.That(() =>  {
                new RangeBuilder().Create();
            }
            , NUnit.Framework.Throws.TypeOf<ArgumentException>());
;
        }

        [NUnit.Framework.Test]
        public virtual void TestFullRange() {
            NUnit.Framework.Assert.IsTrue(RangeBuilder.GetFullRange().Contains(new Random().Next()));
            NUnit.Framework.Assert.IsTrue(RangeBuilder.GetFullRange().Equals(RangeBuilder.GetFullRange()));
            NUnit.Framework.Assert.IsTrue(RangeBuilder.GetFullRange() == RangeBuilder.GetFullRange());
            NUnit.Framework.Assert.IsFalse(RangeBuilder.GetFullRange().Equals(new RangeBuilder().AddRange(1).Create())
                );
        }

        [NUnit.Framework.Test]
        public virtual void TestHashCodeAndEquals() {
            Range range = new RangeBuilder((char)25, (char)26).AddRange(1, 5).AddRange(4, 7).Create();
            Range range2 = new RangeBuilder(25, 26).AddRange((char)1, (char)7).Create();
            NUnit.Framework.Assert.IsTrue(range.GetHashCode() == range2.GetHashCode());
            NUnit.Framework.Assert.IsTrue(range.Equals(range2));
            NUnit.Framework.Assert.AreEqual(range.ToString(), range2.ToString());
            Range range3 = new RangeBuilder(25).AddRange((char)26).AddRange((char)1, (char)7).Create();
            NUnit.Framework.Assert.IsFalse(range2.GetHashCode() == range3.GetHashCode());
            NUnit.Framework.Assert.IsFalse(range2.Equals(range3));
            NUnit.Framework.Assert.AreNotEqual(range2.ToString(), range3.ToString());
            Range range4 = new RangeBuilder(26).AddRange((char)25).AddRange((char)1, (char)4).AddRange((char)3, (char)
                7).Create();
            NUnit.Framework.Assert.IsTrue(range3.GetHashCode() == range4.GetHashCode());
            NUnit.Framework.Assert.IsTrue(range3.Equals(range4));
            NUnit.Framework.Assert.AreEqual(range3.ToString(), range4.ToString());
        }

        [NUnit.Framework.Test]
        public virtual void TestUnionAndContains() {
            Range range = new RangeBuilder((char)25, (char)27).AddRange(2, 10).AddRange(0, 20).AddRange(1, 19).AddRange
                (33, 40).AddRange(0, 5).AddRange(20, 22).AddRange(8, 15).AddRange(25, 30).Create();
            NUnit.Framework.Assert.AreEqual("[(0; 22), (25; 30), (33; 40)]", range.ToString());
            NUnit.Framework.Assert.IsTrue(range.Contains(0));
            NUnit.Framework.Assert.IsTrue(range.Contains(10));
            NUnit.Framework.Assert.IsTrue(range.Contains(22));
            NUnit.Framework.Assert.IsTrue(range.Contains(25));
            NUnit.Framework.Assert.IsTrue(range.Contains(27));
            NUnit.Framework.Assert.IsTrue(range.Contains(30));
            NUnit.Framework.Assert.IsTrue(range.Contains(33));
            NUnit.Framework.Assert.IsTrue(range.Contains(34));
            NUnit.Framework.Assert.IsTrue(range.Contains(40));
            NUnit.Framework.Assert.IsFalse(range.Contains(-1));
            NUnit.Framework.Assert.IsFalse(range.Contains(23));
            NUnit.Framework.Assert.IsFalse(range.Contains(31));
            NUnit.Framework.Assert.IsFalse(range.Contains(32));
            NUnit.Framework.Assert.IsFalse(range.Contains(41));
        }

        [NUnit.Framework.Test]
        public virtual void TestSingles() {
            Range range = new RangeBuilder((char)1).AddRange(2).AddRange(3).AddRange(6).Create();
            NUnit.Framework.Assert.AreEqual("[(1; 1), (2; 2), (3; 3), (6; 6)]", range.ToString());
            NUnit.Framework.Assert.IsTrue(range.Contains(1));
            NUnit.Framework.Assert.IsTrue(range.Contains(2));
            NUnit.Framework.Assert.IsTrue(range.Contains(3));
            NUnit.Framework.Assert.IsTrue(range.Contains(6));
            NUnit.Framework.Assert.IsFalse(range.Contains(0));
            NUnit.Framework.Assert.IsFalse(range.Contains(5));
            NUnit.Framework.Assert.IsFalse(range.Contains(7));
        }
    }
}