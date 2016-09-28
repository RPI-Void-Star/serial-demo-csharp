using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_com_test
{
  class Program
  {
    static void Main(string[] args)
    {
      String[] ports = SerialPort.GetPortNames();
      Console.WriteLine("Listing existing COM ports...");
      for (uint i = 0; i < ports.Length; i++) {
        Console.WriteLine((i + 1) + ":" + ports[i]);
      }
      uint selected = 0;
      while (selected == 0) {
        String input = Console.ReadLine();
        if (!uint.TryParse(input, out selected)) {
          Console.WriteLine("Please enter a valid number");
        }
        if (selected > ports.Length) {
          Console.WriteLine("Please enter a number between 1 and " + ports.Length);
          selected = 0;
        }
      }
      Console.WriteLine("Selected " + ports[selected - 1]);
      SerialPort port = new SerialPort(ports[selected - 1]);
      port.Open();
      // Add DataReceived Handler to Serial Port
      port.DataReceived += new SerialDataReceivedEventHandler (
        (sender, serial_args) => { Console.Write(port.ReadExisting()); }
      );
      while (port.IsOpen) {
        // Check for any available keystrokes
        while (Console.KeyAvailable) {
          // Print all keystrokes to the serial terminal
          port.Write(Console.ReadKey(true).KeyChar.ToString());
        }
      }
    }
  }
}
