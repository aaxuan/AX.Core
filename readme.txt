基于 .Net standard 2.0.3 的通用框架类库。

原则：
🎖 开箱即用，极少代码配置
🎖 尽量减少第三方依赖
🎖 先注重功能，再注重性能
🎖 尽量覆盖 web/客户端 双端支持

⚠ 
依赖包 System.ComponentModel.Annotations >= 4.7.0
依赖包 Newtonsoft.Json >= 12.0.3

⚠ 
使用本类库请一定使用全局异常拦截，基础代码中极少使用 try，大多数参数不合法和意外情况会直接抛出异常。
可对异常类型进行判断限制本类库异常抛出。

⚠ 
使用 Business 内置逻辑


= Business		实验内容

√ Cache			提供了统一管理全局内存缓存的能力。可以显示缓存注册名称，类型，创建时间，数量。
√ CommonModel	提供一些基础的通用实体或框架实体。
√ Config		提供读取 json 配置文件的能力。
= DataBase
√ Encryption	提供加密方法
√ Extension		提供扩展方法
√ Helper		提供帮助类
√ Net			提供邮件，网络请求等帮助方法
√ Reflection	提供简单的反射帮助方法。暂无改进计划。
√ RunLog		提供简易的日志系统。目前支持控制台日志和文件日志。

