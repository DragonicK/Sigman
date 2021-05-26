using System;
using System.IO;
using System.Text;

namespace Sigman.Core.Network {
    public sealed class ByteBuffer {
        readonly MemoryStream buffer;
        int readPos = 0;
        int receivedWritePos = 0;

        private const int ByteLength = 1;
        private const int Int16Length = 2;
        private const int Int32Length = 4;
        private const int Int64Length = 8;

        public ByteBuffer() {
            buffer = new MemoryStream(byte.MaxValue + 1);
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
            return (int)(buffer.Length - readPos);
        }

        public void Flush() {
            buffer.Flush();
            buffer.SetLength(0);
            buffer.Position = 0;
            readPos = 0;
            receivedWritePos = 0;
        }

        public void Trim() {
            if (readPos >= buffer.Length) {
                Flush();
            }
        }

        public void Write(byte[] values) {
            buffer.Write(values, 0, values.Length);
        }

        /// <summary>
        /// Escreve no pacote os dados recebidos pela conexão.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="size"></param>
        public void WriteReceivedBytes(byte[] values, int size) {
            if (buffer.Length + size > buffer.Capacity) {
                buffer.Capacity = (int)(buffer.Length + size);
            }

            buffer.Position = receivedWritePos;
            buffer.Write(values, 0, size);
            receivedWritePos += size;
        }

        public void Write(byte value) {
            var values = new byte[ByteLength];
            values[0] = value;

            buffer.Write(values, 0, ByteLength);
        }

        public void Write(short value) {
            buffer.Write(BitConverter.GetBytes(value), 0, Int16Length);
        }

        public void Write(int value) {
            buffer.Write(BitConverter.GetBytes(value), 0, Int32Length);
        }

        public void Write(long value) {
            buffer.Write(BitConverter.GetBytes(value), 0, Int64Length);
        }

        public void Write(string value) {
            var values = Encoding.Unicode.GetBytes(value);

            Write(values.Length);
            Write(values);
        }

        public byte[] ReadBytes(int length, bool peek = true) {
            var values = new byte[length];

            buffer.Position = readPos;
            buffer.Read(values, 0, length);

            if (peek) {
                readPos += length;
            }

            return values;
        }

        public byte ReadByte(bool peek = true) {
            buffer.Position = readPos;

            if (peek) {
                readPos += ByteLength;
            }

            return (byte)buffer.ReadByte();
        }

        public short ReadInt16(bool peek = true) {
            var values = new byte[Int16Length];

            buffer.Position = readPos;
            buffer.Read(values, 0, Int16Length);

            if (peek) {
                readPos += Int16Length;
            }

            return BitConverter.ToInt16(values, 0);
        }

        public int ReadInt32(bool peek = true) {
            var values = new byte[Int32Length];

            buffer.Position = readPos;
            buffer.Read(values, 0, Int32Length);

            if (peek) {
                readPos += Int32Length;
            }

            return BitConverter.ToInt32(values, 0);
        }

        public long ReadInt64(bool peek = true) {
            var values = new byte[Int64Length];

            buffer.Position = readPos;
            buffer.Read(values, 0, Int64Length);

            if (peek) {
                readPos += Int64Length;
            }

            return BitConverter.ToInt64(values, 0);

        }

        public string ReadString() {
            try {
                var length = ReadInt32();
                return Encoding.Unicode.GetString(ReadBytes(length)); 
            }
            catch {
                return string.Empty;
            }
        }
    }
}