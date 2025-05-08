using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Machine.Tests
{
    public interface IInputReader
    {
        string ReadInput();
    }

    public class ConsoleInputReader : IInputReader
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }

    public class Program
    {
        private readonly IInputReader _inputReader;

        public Program(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public int CheckAlternation(string input)
        {
            // Логика проверки перемежающихся символов
            if (string.IsNullOrEmpty(input))
                return -1; // Или другое значение для пустой строки

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                    return 0; // Не перемежающиеся
            }
            return 1; // Перемежающиеся
        }

        public void Run()
        {
            string strInput = _inputReader.ReadInput();
            int result = CheckAlternation(strInput);
            Console.WriteLine(result);
        }
    }
    public class MockInputReader : IInputReader
    {
        private readonly string _input;

        public MockInputReader(string input)
        {
            _input = input;
        }

        public string ReadInput()
        {
            return _input;
        }
    }
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_AlternatingCharacters()
        {
            var mockInputReader = new MockInputReader("abab");
            var program = new Program(mockInputReader);

            int result = program.CheckAlternation("abab");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_NonAlternatingCharacters()
        {
            var mockInputReader = new MockInputReader("aa");
            var program = new Program(mockInputReader);

            int result = program.CheckAlternation("aa");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_EmptyString()
        {
            var mockInputReader = new MockInputReader("");
            var program = new Program(mockInputReader);

            int result = program.CheckAlternation("");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Test_SingleCharacter()
        {
            var mockInputReader = new MockInputReader("a");
            var program = new Program(mockInputReader);

            int result = program.CheckAlternation("a");
            Assert.AreEqual(1, result);
        }
    }
}
