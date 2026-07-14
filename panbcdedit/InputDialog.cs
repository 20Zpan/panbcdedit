using System;
using System.Drawing;
using System.Windows.Forms;

namespace panbcdedit
{
    /// <summary>
    /// 简单的输入对话框，避免引用 Microsoft.VisualBasic。
    /// </summary>
    public static class InputDialog
    {
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            using (Form form = new Form())
            {
                form.Text = title;
                form.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new Size(440, 140);
                form.Padding = new Padding(10);
                form.AutoScaleMode = AutoScaleMode.Font;

                Label label = new Label
                {
                    Text = prompt,
                    AutoSize = true,
                    Location = new Point(12, 12)
                };

                TextBox textBox = new TextBox
                {
                    Text = defaultValue,
                    Location = new Point(12, label.Bottom + 10),
                    Width = 400,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Button okButton = new Button
                {
                    Text = "确定",
                    DialogResult = DialogResult.OK,
                    Location = new Point(230, textBox.Bottom + 20),
                    Height = 27,
                    Width = 80
                };

                Button cancelButton = new Button
                {
                    Text = "取消",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(320, okButton.Top),
                    Height = 27,
                    Width = 80
                };

                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(okButton);
                form.Controls.Add(cancelButton);

                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }
    }
}
