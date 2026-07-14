# panbcdedit

Windows BCD（启动配置数据）图形化管理工具，以可视化的方式替代繁琐的 bcdedit 命令行操作。

[下载链接https://github.com/20Zpan/panbcdedit/releases](https://github.com/20Zpan/panbcdedit/releases)

> 本工具主要面向 **UEFI + GPT** 环境设计。
> 传统 BIOS + MBR 分区下基础功能可用，但部分特性（如加载外部 .efi 引导文件）会因缺少 EFI 系统分区而受限。

## 功能一览

| 功能模块 | 说明 |
|---|---|
| **启动项总览** | 查看所有启动项的完整 BCD 配置，支持全量/精简模式切换 |
| **启动项管理** | 复制、删除、新建启动项，支持五种条目类型，含高级模式精细配置 |
| **常见启动项设置** | 设置默认启动项、超时时间、菜单显示顺序、BCD 导入/导出 |
| **高级开关** | 对单个启动项精细控制（测试签名、调试、驱动签名检测、PAE、NX、Hyper-V 等） |
| **引导修复** | 基于 bcdboot 的一键修复工具，支持固件类型选择和语言设置 |

## 环境要求

- **操作系统**: Windows Vista ~ Windows 11（64 位）
- **运行库**: .NET Framework 4.8（Win10/Win11 自带，Win7 SP1 需单独安装）

## 快速开始

下载 `release/panbcdedit.exe` 直接运行即可（需要 .NET Framework 4.8）。

## 截图

![启动项总览](photo/1%E5%90%AF%E5%8A%A8%E9%A1%B9%E6%80%BB%E8%A7%88.png)

![启动项管理](photo/2%E5%90%AF%E5%8A%A8%E9%A1%B9%E7%AE%A1%E7%90%86.png)

![常见启动项设置](photo/3%E5%B8%B8%E8%A7%81%E5%90%AF%E5%8A%A8%E9%A1%B9%E7%AE%A1%E7%90%86.png)

![引导修复与新手指南](photo/4%E5%BC%95%E5%AF%BC%E4%BF%AE%E5%A4%8D-%E6%96%B0%E6%89%8B%E6%8C%87%E5%8D%97.png)

## 技术说明

- 所有 BCD 操作底层调用 `bcdedit.exe` 和 `bcdboot.exe`，无额外依赖
- 日志自动写入 `%temp%\panbcdedit\log\`，按天分文件，便于问题排查
- 差异化执行策略：高级开关仅提交有变化的设置项，避免无效操作

### 加载外部 UEFI 引导文件

创建启动项时支持 **Linux/UEFI Boot Loader** 类型，用于加载第三方 UEFI 引导文件（如 rEFInd、GRUB等）。

原理说明：
1. 工具会自动查找并挂载 EFI 系统分区（ESP）
2. 将内置的 `efiloader.efi` 部署到 ESP 的 `\EFI\pan\` 目录
3. 在 BCD 中创建一条启动项，通过 `efiloader.efi` 作为跳板加载用户指定的目标 .efi 文件

这种方式绕过了 Windows Boot Manager 的签名限制，首次创建时会自动完成 efiloader.efi 的部署，后续可直接使用。

> **架构说明**：内置的 efiloader.efi 仅支持 **amd64（x86_64）** 架构的 CPU。
> 如果你的设备是 ia32 或 arm64 等其他架构，可前往 `https://github.com/a1ive/efiloader/releases/tag/v0.1` 开源项目下载对应架构的 efiloader.efi 自行替换。
> 这里感谢大佬的efiloader项目帮助我在bcd中启动外部.efi文件。

## 免责声明

修改 BCD 配置可能导致系统无法正常启动。操作前请务必导出备份。本工具按"原样"提供，作者不对因使用本工具造成的任何损失承担责任。
