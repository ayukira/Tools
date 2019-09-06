namespace AutofacTool
{
    public enum InstanceType
    {
        /// <summary>
        /// 默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象
        /// </summary>
        PerDependency = 1,
        /// <summary>
        /// 同一个Lifetime生成的对象是同一个实例
        /// </summary>
        PerLifetimeScope = 2,
        /// <summary>
        /// 单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象
        /// </summary>
        Single = 3
    }
}
