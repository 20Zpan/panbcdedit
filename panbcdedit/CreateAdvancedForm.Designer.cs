namespace panbcdedit
{
    partial class CreateAdvancedForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeValueLabel = new System.Windows.Forms.Label();
            this.sepLabel = new System.Windows.Forms.Label();
            this.paramsGroupBox = new System.Windows.Forms.GroupBox();
            this.paramsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.buttonFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.mainTableLayout.SuspendLayout();
            this.paramsGroupBox.SuspendLayout();
            this.buttonFlow.SuspendLayout();
            this.SuspendLayout();

            // mainTableLayout
            this.mainTableLayout.ColumnCount = 2;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.typeLabel, 0, 0);
            this.mainTableLayout.Controls.Add(this.typeValueLabel, 1, 0);
            this.mainTableLayout.Controls.Add(this.sepLabel, 0, 1);
            this.mainTableLayout.SetColumnSpan(this.sepLabel, 2);
            this.mainTableLayout.Controls.Add(this.paramsGroupBox, 0, 2);
            this.mainTableLayout.SetColumnSpan(this.paramsGroupBox, 2);
            this.mainTableLayout.Controls.Add(this.buttonFlow, 0, 3);
            this.mainTableLayout.SetColumnSpan(this.buttonFlow, 2);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.Padding = new System.Windows.Forms.Padding(10);
            this.mainTableLayout.RowCount = 4;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.mainTableLayout.Size = new System.Drawing.Size(560, 400);
            this.mainTableLayout.TabIndex = 0;

            // typeLabel
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(13, 13);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(41, 12);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "类型：";

            // typeValueLabel
            this.typeValueLabel.AutoSize = true;
            this.typeValueLabel.Location = new System.Drawing.Point(113, 13);
            this.typeValueLabel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.typeValueLabel.Name = "typeValueLabel";
            this.typeValueLabel.Size = new System.Drawing.Size(41, 12);
            this.typeValueLabel.TabIndex = 1;
            this.typeValueLabel.Text = "Windows Boot Loader";

            // sepLabel
            this.sepLabel.AutoSize = false;
            this.sepLabel.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.sepLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sepLabel.Location = new System.Drawing.Point(10, 38);
            this.sepLabel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.sepLabel.Name = "sepLabel";
            this.sepLabel.Size = new System.Drawing.Size(540, 2);
            this.sepLabel.TabIndex = 2;
            this.sepLabel.Text = "";

            // paramsGroupBox
            this.paramsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsGroupBox.Location = new System.Drawing.Point(10, 48);
            this.paramsGroupBox.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.paramsGroupBox.Name = "paramsGroupBox";
            this.paramsGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.paramsGroupBox.Size = new System.Drawing.Size(540, 300);
            this.paramsGroupBox.TabIndex = 5;
            this.paramsGroupBox.TabStop = false;
            this.paramsGroupBox.Text = "参数设置";
            // GroupBox 内套一个 Panel 提供滚动能力
            System.Windows.Forms.Panel scrollPanel = new System.Windows.Forms.Panel();
            scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            scrollPanel.AutoScroll = true;
            scrollPanel.Padding = new System.Windows.Forms.Padding(10, 6, 10, 10);
            scrollPanel.Controls.Add(this.paramsTableLayout);
            this.paramsGroupBox.Controls.Add(scrollPanel);

            // paramsTableLayout
            this.paramsTableLayout.AutoSize = true;
            this.paramsTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.paramsTableLayout.ColumnCount = 1;
            this.paramsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.paramsTableLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.paramsTableLayout.Location = new System.Drawing.Point(0, 35);
            this.paramsTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.paramsTableLayout.Name = "paramsTableLayout";
            this.paramsTableLayout.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.paramsTableLayout.RowCount = 0;
            this.paramsTableLayout.Size = new System.Drawing.Size(530, 0);
            this.paramsTableLayout.TabIndex = 2;

            // buttonFlow
            this.buttonFlow.Controls.Add(this.cancelButton);
            this.buttonFlow.Controls.Add(this.createButton);
            this.buttonFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonFlow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonFlow.Location = new System.Drawing.Point(10, 350);
            this.buttonFlow.Margin = new System.Windows.Forms.Padding(0);
            this.buttonFlow.Name = "buttonFlow";
            this.buttonFlow.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.buttonFlow.Size = new System.Drawing.Size(540, 40);
            this.buttonFlow.TabIndex = 6;

            // cancelButton
            this.cancelButton.AutoSize = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(460, 5);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 27);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;

            // createButton
            this.createButton.AutoSize = false;
            this.createButton.Location = new System.Drawing.Point(380, 5);
            this.createButton.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 27);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "创建启动项";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);

            // CreateAdvancedForm
            this.AcceptButton = this.createButton;
            this.CancelButton = this.cancelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(560, 400);
            this.Controls.Add(this.mainTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateAdvancedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自定义创建启动项";
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.paramsGroupBox.ResumeLayout(false);
            this.paramsGroupBox.PerformLayout();
            this.buttonFlow.ResumeLayout(false);
            this.buttonFlow.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label typeValueLabel;
        private System.Windows.Forms.Label sepLabel;
        private System.Windows.Forms.GroupBox paramsGroupBox;
        private System.Windows.Forms.TableLayoutPanel paramsTableLayout;
        private System.Windows.Forms.FlowLayoutPanel buttonFlow;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button createButton;
    }
}
