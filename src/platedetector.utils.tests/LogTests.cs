using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Platedetector.Utils.Logging.Tests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void Info_WithEmptyMsg_ThrowsArgumentException()
        {
            var log = new Log();
            var msg = string.Empty;


            var action = new Action(()=>
            {
                log.Info(msg);
            });


            Assert.ThrowsException<ArgumentException>(action);
            log.Dispose();
        }

        [TestMethod]
        public void Info_WithNullMsg_ThrowsArgumentNullException()
        {
            var log = new Log();
            string msg = null;


            var action = new Action(() =>
            {
                log.Info(msg);
            });


            Assert.ThrowsException<ArgumentNullException>(action);
            log.Dispose();
        }

        [TestMethod]
        public void Info_WithNormalMessage_WritesMessage()
        {
            var log = new Log();
            string msg = "Test123<>";


            log.Info(msg);
            var field = typeof(Log).GetField(
                "_streamWriter",
                BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
            var writer = field.GetValue(log) as StreamWriter;
            var buffer = typeof(StreamWriter)
                .GetField("charBuffer", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance)
                .GetValue(writer) as char[];
            var result = new StringBuilder()
                .Append(buffer.Where(e => e != '\0').ToArray())
                .ToString();
            

            Assert.AreEqual(new LogInfoMessage(msg).FormattedMessage, result.Trim());
            log.Dispose();
        }
    }
}
