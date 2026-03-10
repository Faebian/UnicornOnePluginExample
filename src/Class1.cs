using System;
using System.Windows.Forms;
using UnicornOne.Workbook;

namespace UnicornOneHelloPlugin
{
    public sealed class HelloWorldPlugin : IWorkbookPlugin, IWorkbookController, IDisposable
    {
        private IWorkbookHost _host;
        private Panel _panel;

        public string Name => "HelloWorldPlugin";
        public string Version => "1.0";

        public void Initialize(string jsonConfig, IWorkbookHost host)
        {
            _host = host;
            _host.AppendLog("Plugin", "Info", "HelloWorldPlugin initialized");
        }

        public void BuildUI()
        {
            _panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };

            _host.Theme.ApplyBaseStyles(_panel);

            var label = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Text = "Hello world from a UnicornOne plugin!"
            };

            var button = new Button
            {
                Dock = DockStyle.Top,
                Text = "Say Hello",
                Height = 32
            };

            button.Click += (s, e) =>
            {
                _host.AppendLog("Plugin", "Info", "Hello button clicked");
                _host.SetStatus("Hello from plugin");
                MessageBox.Show("Hello world from UnicornOne plugin!", "Hello");
            };

            _panel.Controls.Add(button);
            _panel.Controls.Add(label);

            _host.AddMenuButton("Run Hello", Run);
            _host.AddWidget(_panel, "Main");
        }

        public void OnShown() { }

        public void OnHidden() { }

        public void Run()
        {
            _host.AppendLog("Plugin", "Info", "Run invoked");
            _host.SetStatus("HelloWorldPlugin executed");
            _host.MarkComplete();
        }

        public void RequestCancel()
        {
            _host.AppendLog("Plugin", "Warning", "Cancel requested, nothing to cancel");
            _host.MarkComplete();
        }

        public void Dispose()
        {
        }
    }
}