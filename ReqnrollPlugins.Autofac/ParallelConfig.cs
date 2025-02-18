// class-level parallel
[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.ClassLevel)]

// method-level parallel
//[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]

// no parallel
//[assembly: DoNotParallelize]