using System;

namespace MyGame.Exceptions
{
    public class AlreadyExistException : Exception
    {
        private const string BaseMessage = "This element already exists in collection";

        public AlreadyExistException() : base(BaseMessage) { }                         // Создаем конструкторы

        public AlreadyExistException(string message) : base(message) { }

        public AlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
