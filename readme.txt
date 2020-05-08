基于 .Net standard 2.0.3 的通用基础类库

使用本类库请一定使用全局异常拦截，基础代码中极少 try，大多数参数不合法和意外情况会直接抛出异常。
可对异常类型进行判断限制本类库异常抛出。

依赖包 System.ComponentModel.Annotations 4.7.0
依赖包 Newtonsoft.Json 12.0.3

若使用 核心内置逻辑层 user基类表命名限定为【base_user】
