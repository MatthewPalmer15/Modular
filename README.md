# Modular


## Installation
1. Download the DLLs within the releases. These contains the Class Library based on the .NET Framework.
2. Once downloaded, right click the "Dependencies" in your solution, and click "Add Project Reference".
3. Click "Browse" tab, and find the Modular DLL. Once you have selected, click "OK".
4. You should now be able to use these classes. Now, within your app.config or web.config, name your database connection string to "ModularDB".

After completing this step, you should now be able to take full advantage of Modular!


## Dependencies/Packages
You MUST have the Modular.Base DLL within your project reference for the other modules to work. This class library also only works with projects using the .NET Framework.

NUGET Packages:
- Microsoft.Data.SqlClient v5.1.1
- Microsoft.Data.Sqlite v7.0.9
- Microsoft.Maui.Graphics v7.0.86
- Syncfusion.DocIO.NET v22.1.38
- Syncfusion.Pdf.NET v22.1.38
- Syncfusion.Presentation.NET v22.1.38
- Syncfusion.XlsIO.NET v22.1.38
- System.Configuration.ConfigurationManager v7.0.0


## Licence

MIT License

Copyright (c) 2023 Matthew Palmer

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
