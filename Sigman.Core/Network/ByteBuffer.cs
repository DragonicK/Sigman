using System;
using System.IO;
using System.Text;

namespace Sigman.Core.Network {
    public sealed class ByteBuffer {
        readonly MemoryStream buffer;
        int readposition = 0;

        private const int ByteLength = 1;
        private const int Int16Length = 2;
        private const int Int32Length = 4;
        private const int Int64Length = 8;

        public ByteBuffer() {
            buffer = new MemoryStream(byte.MaxValue);
        }

        public ByteBuffer(int capacity) {
            buffer = new MemoryStream(capacity);
        }

        public ByteBuffer(byte[] arr) {
            buffer = new MemoryStream(arr, 0, arr.Length);
        }

        public ByteBuffer(byte[] arr, int count) {
            buffer = new MemoryStream(arr, 0, count);
        }

        public byte[] ToArray() {
            return buffer.ToArray();
        }

        public int Length() {
            return (int)(buffer.Length - readposition);
        }

        public void Flush() {
            buffer.Flush();
            buffer.SetLength(0);
            buffer.Position = 0;
            readposition = 0;
        }

        public void Trim() {
            if (readposition >= buffer.Length) {
                Flush();
            }
        }

        public void Write(byte[] values) {
            CheckCapacity(values.Length);

            buffer.Write(values, 0, values.Length);
        }

        public void Write(byte value) {
            CheckCapacity(ByteLength);

            var values = new byte[ByteLength];
            values[0] = value;

            buffer.Write(values, 0, ByteLength);
        }

        public void Write(short value) {
            CheckCapacity(Int16Length);

            buffer.Write(BitConverter.GetBytes(value), 0, Int16Length);
        }

        public void Write(int value) {
            CheckCapacity(Int32Length);

            buffer.Write(BitConverter.GetBytes(value), 0, Int32Length);
        }

        public void Write(long value) {
            CheckCapacity(Int64Length);

            buffer.Write(BitConverter.GetBytes(value), 0, Int64Length);
        }

        public void Write(string value) {
            var values = Encoding.UTF32.GetBytes(value);

            Write(value.Length);
            Write(values);
        }

        public byte[] ReadBytes(int length, bool peek = true) {
            var values = new byte[length];

            buffer.Position = readposition;
            buffer.Read(values, 0, length);

            if (peek) {
                readposition += length;
            }

            return values;
        }

        public byte[] ReadBytes(bool peek = true) {
            int length = ReadInt32();
            var values = new byte[length];

            buffer.Position = readposition;
            buffer.Read(values, 0, length);

            if (peek) {
                readposition += length;
            }

            return values;
        }

        public byte ReadByte(bool peek = true) {
            buffer.Position = readposition;

            if (peek) {
                readposition += ByteLength;
            }

            return (byte)buffer.ReadByte();
        }

        public short ReadInt16(bool peek = true) {
            var values = new byte[Int16Length];

            buffer.Position = readposition;
            buffer.Read(values, 0, Int16Length);

            if (peek) {
                readposition += Int16Length;
            }

            return BitConverter.ToInt16(values, 0);
        }

        public int ReadInt32(bool peek = true) {
            var values = new byte[Int32Length];

            buffer.Position = readposition;
            buffer.Read(values, 0, Int32Length);

            if (peek) {
                readposition += Int32Length;
            }

            return BitConverter.ToInt32(values, 0);
        }

        public string ReadString(bool peek = true) {
            try {
                var length = ReadInt32();

                buffer.Position = readposition;

                var value = Encoding.UTF32.GetString(buffer.ToArray(), readposition, length);

                if (peek) {
                    readposition += length;
                }

                return value;
            }
            catch {
                return string.Empty;
            }
        }

        private void CheckCapacity(int size) {
            if (buffer.Length + size > buffer.Capacity) {
                buffer.Capacity = (int)(buffer.Length + size);
            }
        }
    }
}