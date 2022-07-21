using TestApp;

object myBestClass = new BestClass();
var myBestClassType = myBestClass.GetType();
var hello = myBestClassType.GetMethod("Hello", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
hello.Invoke(myBestClass, null);