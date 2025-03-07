using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services
{
    public class DataRepeater<T>
    {
        readonly Channel<T> DataChannel = Channel.CreateUnbounded<T>();

        public async Task WriteDataAsync(T data)
        {
            await DataChannel.Writer.WriteAsync(data);
        }

        public async Task ParseMessageAsync(Action<T> parseAction)
        {
            while (await DataChannel.Reader.WaitToReadAsync())
            {
                if (DataChannel.Reader.TryRead(out var message))
                    parseAction?.Invoke(message);
            }
        }
    }
}
