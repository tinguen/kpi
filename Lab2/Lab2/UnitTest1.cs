using System;
using Xunit;
using IIG.BinaryFlag;

namespace Lab2
{
    public class UnitTest1
    {
        [Fact]
        public void testLongLengthInput()
        {
            MultipleBinaryFlag multipleBinaryFlag1 = new MultipleBinaryFlag(ulong.MaxValue, false);
            MultipleBinaryFlag multipleBinaryFlag2 = new MultipleBinaryFlag(ulong.MaxValue - 1, false);

            Assert.False(multipleBinaryFlag1.GetFlag());
            Assert.False(multipleBinaryFlag2.GetFlag());
        }

        [Fact]
        public void testMinimumLengthInput()
        {
            MultipleBinaryFlag multipleBinaryFlag1 = new MultipleBinaryFlag(ulong.MinValue, false);
            MultipleBinaryFlag multipleBinaryFlag2 = new MultipleBinaryFlag(ulong.MinValue + 1, false);

            Assert.False(multipleBinaryFlag1.GetFlag());
            Assert.False(multipleBinaryFlag2.GetFlag());
        }

        [Fact]
        public void testZeroLengthInput()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(0, false));
        }

        [Fact]
        public void testDispose()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, false);

            multipleBinaryFlag.Dispose();

            Assert.Null(multipleBinaryFlag);
        }

        [Fact]
        public void testGetFlagAllFalse()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, false);

            Assert.False(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void testGetFlagAllTrue()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, true);

            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void testSetFlag()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, false);

            for (ulong i = 0; i < 10; i++) {
                multipleBinaryFlag.SetFlag(i);
            }

            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void testSetFlagThrowsError()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, false);

            Assert.Throws<ArgumentOutOfRangeException>(() => multipleBinaryFlag.SetFlag(10));
        }

        [Fact]
        public void testResetFlag()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, true);

            multipleBinaryFlag.ResetFlag(0);

            Assert.False(multipleBinaryFlag.GetFlag());
        }

        [Fact]
        public void testResetFlagThrowsError()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, true);

            Assert.Throws<ArgumentOutOfRangeException>(() => multipleBinaryFlag.ResetFlag(10));
        }
    }
}
