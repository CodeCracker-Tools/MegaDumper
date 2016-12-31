using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace ProcessStatistics
{

    		        
  /// <summary>
    /// A NT status value.
    /// </summary>
    public enum NtStatus : uint
    {
        // Success
        Success = 0x00000000,
        Wait0 = 0x00000000,
        Wait1 = 0x00000001,
        Wait2 = 0x00000002,
        Wait3 = 0x00000003,
        Wait63 = 0x0000003f,
        Abandoned = 0x00000080,
        AbandonedWait0 = 0x00000080,
        AbandonedWait1 = 0x00000081,
        AbandonedWait2 = 0x00000082,
        AbandonedWait3 = 0x00000083,
        AbandonedWait63 = 0x000000bf,
        UserApc = 0x000000c0,
        KernelApc = 0x00000100,
        Alerted = 0x00000101,
        Timeout = 0x00000102,
        Pending = 0x00000103,
        Reparse = 0x00000104,
        MoreEntries = 0x00000105,
        NotAllAssigned = 0x00000106,
        SomeNotMapped = 0x00000107,
        OpLockBreakInProgress = 0x00000108,
        VolumeMounted = 0x00000109,
        RxActCommitted = 0x0000010a,
        NotifyCleanup = 0x0000010b,
        NotifyEnumDir = 0x0000010c,
        NoQuotasForAccount = 0x0000010d,
        PrimaryTransportConnectFailed = 0x0000010e,
        PageFaultTransition = 0x00000110,
        PageFaultDemandZero = 0x00000111,
        PageFaultCopyOnWrite = 0x00000112,
        PageFaultGuardPage = 0x00000113,
        PageFaultPagingFile = 0x00000114,
        CrashDump = 0x00000116,
        ReparseObject = 0x00000118,
        NothingToTerminate = 0x00000122,
        ProcessNotInJob = 0x00000123,
        ProcessInJob = 0x00000124,
        ProcessCloned = 0x00000129,
        FileLockedWithOnlyReaders = 0x0000012a,
        FileLockedWithWriters = 0x0000012b,

        // Informational
        Informational = 0x40000000,
        ObjectNameExists = 0x40000000,
        ThreadWasSuspended = 0x40000001,
        WorkingSetLimitRange = 0x40000002,
        ImageNotAtBase = 0x40000003,
        RegistryRecovered = 0x40000009,

        // Warning
        Warning = 0x80000000,
        GuardPageViolation = 0x80000001,
        DatatypeMisalignment = 0x80000002,
        Breakpoint = 0x80000003,
        SingleStep = 0x80000004,
        BufferOverflow = 0x80000005,
        NoMoreFiles = 0x80000006,
        HandlesClosed = 0x8000000a,
        PartialCopy = 0x8000000d,
        DeviceBusy = 0x80000011,
        InvalidEaName = 0x80000013,
        EaListInconsistent = 0x80000014,
        NoMoreEntries = 0x8000001a,
        LongJump = 0x80000026,
        DllMightBeInsecure = 0x8000002b,

        // Error
        Error = 0xc0000000,
        Unsuccessful = 0xc0000001,
        NotImplemented = 0xc0000002,
        InvalidInfoClass = 0xc0000003,
        InfoLengthMismatch = 0xc0000004,
        AccessViolation = 0xc0000005,
        InPageError = 0xc0000006,
        PagefileQuota = 0xc0000007,
        InvalidHandle = 0xc0000008,
        BadInitialStack = 0xc0000009,
        BadInitialPc = 0xc000000a,
        InvalidCid = 0xc000000b,
        TimerNotCanceled = 0xc000000c,
        InvalidParameter = 0xc000000d,
        NoSuchDevice = 0xc000000e,
        NoSuchFile = 0xc000000f,
        InvalidDeviceRequest = 0xc0000010,
        EndOfFile = 0xc0000011,
        WrongVolume = 0xc0000012,
        NoMediaInDevice = 0xc0000013,
        NoMemory = 0xc0000017,
        NotMappedView = 0xc0000019,
        UnableToFreeVm = 0xc000001a,
        UnableToDeleteSection = 0xc000001b,
        IllegalInstruction = 0xc000001d,
        AlreadyCommitted = 0xc0000021,
        AccessDenied = 0xc0000022,
        BufferTooSmall = 0xc0000023,
        ObjectTypeMismatch = 0xc0000024,
        NonContinuableException = 0xc0000025,
        BadStack = 0xc0000028,
        NotLocked = 0xc000002a,
        NotCommitted = 0xc000002d,
        InvalidParameterMix = 0xc0000030,
        ObjectNameInvalid = 0xc0000033,
        ObjectNameNotFound = 0xc0000034,
        ObjectNameCollision = 0xc0000035,
        ObjectPathInvalid = 0xc0000039,
        ObjectPathNotFound = 0xc000003a,
        ObjectPathSyntaxBad = 0xc000003b,
        DataOverrun = 0xc000003c,
        DataLate = 0xc000003d,
        DataError = 0xc000003e,
        CrcError = 0xc000003f,
        SectionTooBig = 0xc0000040,
        PortConnectionRefused = 0xc0000041,
        InvalidPortHandle = 0xc0000042,
        SharingViolation = 0xc0000043,
        QuotaExceeded = 0xc0000044,
        InvalidPageProtection = 0xc0000045,
        MutantNotOwned = 0xc0000046,
        SemaphoreLimitExceeded = 0xc0000047,
        PortAlreadySet = 0xc0000048,
        SectionNotImage = 0xc0000049,
        SuspendCountExceeded = 0xc000004a,
        ThreadIsTerminating = 0xc000004b,
        BadWorkingSetLimit = 0xc000004c,
        IncompatibleFileMap = 0xc000004d,
        SectionProtection = 0xc000004e,
        EasNotSupported = 0xc000004f,
        EaTooLarge = 0xc0000050,
        NonExistentEaEntry = 0xc0000051,
        NoEasOnFile = 0xc0000052,
        EaCorruptError = 0xc0000053,
        FileLockConflict = 0xc0000054,
        LockNotGranted = 0xc0000055,
        DeletePending = 0xc0000056,
        CtlFileNotSupported = 0xc0000057,
        UnknownRevision = 0xc0000058,
        RevisionMismatch = 0xc0000059,
        InvalidOwner = 0xc000005a,
        InvalidPrimaryGroup = 0xc000005b,
        NoImpersonationToken = 0xc000005c,
        CantDisableMandatory = 0xc000005d,
        NoLogonServers = 0xc000005e,
        NoSuchLogonSession = 0xc000005f,
        NoSuchPrivilege = 0xc0000060,
        PrivilegeNotHeld = 0xc0000061,
        InvalidAccountName = 0xc0000062,
        UserExists = 0xc0000063,
        NoSuchUser = 0xc0000064,
        GroupExists = 0xc0000065,
        NoSuchGroup = 0xc0000066,
        MemberInGroup = 0xc0000067,
        MemberNotInGroup = 0xc0000068,
        LastAdmin = 0xc0000069,
        WrongPassword = 0xc000006a,
        IllFormedPassword = 0xc000006b,
        PasswordRestriction = 0xc000006c,
        LogonFailure = 0xc000006d,
        AccountRestriction = 0xc000006e,
        InvalidLogonHours = 0xc000006f,
        InvalidWorkstation = 0xc0000070,
        PasswordExpired = 0xc0000071,
        AccountDisabled = 0xc0000072,
        NoneMapped = 0xc0000073,
        TooManyLuidsRequested = 0xc0000074,
        LuidsExhausted = 0xc0000075,
        InvalidSubAuthority = 0xc0000076,
        InvalidAcl = 0xc0000077,
        InvalidSid = 0xc0000078,
        InvalidSecurityDescr = 0xc0000079,
        ProcedureNotFound = 0xc000007a,
        InvalidImageFormat = 0xc000007b,
        NoToken = 0xc000007c,
        BadInheritanceAcl = 0xc000007d,
        RangeNotLocked = 0xc000007e,
        DiskFull = 0xc000007f,
        ServerDisabled = 0xc0000080,
        ServerNotDisabled = 0xc0000081,
        TooManyGuidsRequested = 0xc0000082,
        GuidsExhausted = 0xc0000083,
        InvalidIdAuthority = 0xc0000084,
        AgentsExhausted = 0xc0000085,
        InvalidVolumeLabel = 0xc0000086,
        SectionNotExtended = 0xc0000087,
        NotMappedData = 0xc0000088,
        ResourceDataNotFound = 0xc0000089,
        ResourceTypeNotFound = 0xc000008a,
        ResourceNameNotFound = 0xc000008b,
        ArrayBoundsExceeded = 0xc000008c,
        FloatDenormalOperand = 0xc000008d,
        FloatDivideByZero = 0xc000008e,
        FloatInexactResult = 0xc000008f,
        FloatInvalidOperation = 0xc0000090,
        FloatOverflow = 0xc0000091,
        FloatStackCheck = 0xc0000092,
        FloatUnderflow = 0xc0000093,
        IntegerDivideByZero = 0xc0000094,
        IntegerOverflow = 0xc0000095,
        PrivilegedInstruction = 0xc0000096,
        TooManyPagingFiles = 0xc0000097,
        FileInvalid = 0xc0000098,
        InstanceNotAvailable = 0xc00000ab,
        PipeNotAvailable = 0xc00000ac,
        InvalidPipeState = 0xc00000ad,
        PipeBusy = 0xc00000ae,
        IllegalFunction = 0xc00000af,
        PipeDisconnected = 0xc00000b0,
        PipeClosing = 0xc00000b1,
        PipeConnected = 0xc00000b2,
        PipeListening = 0xc00000b3,
        InvalidReadMode = 0xc00000b4,
        IoTimeout = 0xc00000b5,
        FileForcedClosed = 0xc00000b6,
        ProfilingNotStarted = 0xc00000b7,
        ProfilingNotStopped = 0xc00000b8,
        NotSameDevice = 0xc00000d4,
        FileRenamed = 0xc00000d5,
        CantWait = 0xc00000d8,
        PipeEmpty = 0xc00000d9,
        CantTerminateSelf = 0xc00000db,
        InternalError = 0xc00000e5,
        InvalidParameter1 = 0xc00000ef,
        InvalidParameter2 = 0xc00000f0,
        InvalidParameter3 = 0xc00000f1,
        InvalidParameter4 = 0xc00000f2,
        InvalidParameter5 = 0xc00000f3,
        InvalidParameter6 = 0xc00000f4,
        InvalidParameter7 = 0xc00000f5,
        InvalidParameter8 = 0xc00000f6,
        InvalidParameter9 = 0xc00000f7,
        InvalidParameter10 = 0xc00000f8,
        InvalidParameter11 = 0xc00000f9,
        InvalidParameter12 = 0xc00000fa,
        MappedFileSizeZero = 0xc000011e,
        TooManyOpenedFiles = 0xc000011f,
        Cancelled = 0xc0000120,
        CannotDelete = 0xc0000121,
        InvalidComputerName = 0xc0000122,
        FileDeleted = 0xc0000123,
        SpecialAccount = 0xc0000124,
        SpecialGroup = 0xc0000125,
        SpecialUser = 0xc0000126,
        MembersPrimaryGroup = 0xc0000127,
        FileClosed = 0xc0000128,
        TooManyThreads = 0xc0000129,
        ThreadNotInProcess = 0xc000012a,
        TokenAlreadyInUse = 0xc000012b,
        PagefileQuotaExceeded = 0xc000012c,
        CommitmentLimit = 0xc000012d,
        InvalidImageLeFormat = 0xc000012e,
        InvalidImageNotMz = 0xc000012f,
        InvalidImageProtect = 0xc0000130,
        InvalidImageWin16 = 0xc0000131,
        LogonServer = 0xc0000132,
        DifferenceAtDc = 0xc0000133,
        SynchronizationRequired = 0xc0000134,
        DllNotFound = 0xc0000135,
        IoPrivilegeFailed = 0xc0000137,
        OrdinalNotFound = 0xc0000138,
        EntryPointNotFound = 0xc0000139,
        ControlCExit = 0xc000013a,
        PortNotSet = 0xc0000353,
        DebuggerInactive = 0xc0000354,
        CallbackBypass = 0xc0000503,
        PortClosed = 0xc0000700,
        MessageLost = 0xc0000701,
        InvalidMessage = 0xc0000702,
        RequestCanceled = 0xc0000703,
        RecursiveDispatch = 0xc0000704,
        LpcReceiveBufferExpected = 0xc0000705,
        LpcInvalidConnectionUsage = 0xc0000706,
        LpcRequestsNotAllowed = 0xc0000707,
        ResourceInUse = 0xc0000708,
        ProcessIsProtected = 0xc0000712,
        VolumeDirty = 0xc0000806,
        FileCheckedOut = 0xc0000901,
        CheckOutRequired = 0xc0000902,
        BadFileType = 0xc0000903,
        FileTooLarge = 0xc0000904,
        FormsAuthRequired = 0xc0000905,
        VirusInfected = 0xc0000906,
        VirusDeleted = 0xc0000907,
        TransactionalConflict = 0xc0190001,
        InvalidTransaction = 0xc0190002,
        TransactionNotActive = 0xc0190003,
        TmInitializationFailed = 0xc0190004,
        RmNotActive = 0xc0190005,
        RmMetadataCorrupt = 0xc0190006,
        TransactionNotJoined = 0xc0190007,
        DirectoryNotRm = 0xc0190008,
        CouldNotResizeLog = 0xc0190009,
        TransactionsUnsupportedRemote = 0xc019000a,
        LogResizeInvalidSize = 0xc019000b,
        RemoteFileVersionMismatch = 0xc019000c,
        CrmProtocolAlreadyExists = 0xc019000f,
        TransactionPropagationFailed = 0xc0190010,
        CrmProtocolNotFound = 0xc0190011,
        TransactionSuperiorExists = 0xc0190012,
        TransactionRequestNotValid = 0xc0190013,
        TransactionNotRequested = 0xc0190014,
        TransactionAlreadyAborted = 0xc0190015,
        TransactionAlreadyCommitted = 0xc0190016,
        TransactionInvalidMarshallBuffer = 0xc0190017,
        CurrentTransactionNotValid = 0xc0190018,
        LogGrowthFailed = 0xc0190019,
        ObjectNoLongerExists = 0xc0190021,
        StreamMiniversionNotFound = 0xc0190022,
        StreamMiniversionNotValid = 0xc0190023,
        MiniversionInaccessibleFromSpecifiedTransaction = 0xc0190024,
        CantOpenMiniversionWithModifyIntent = 0xc0190025,
        CantCreateMoreStreamMiniversions = 0xc0190026,
        HandleNoLongerValid = 0xc0190028,
        NoTxfMetadata = 0xc0190029,
        LogCorruptionDetected = 0xc0190030,
        CantRecoverWithHandleOpen = 0xc0190031,
        RmDisconnected = 0xc0190032,
        EnlistmentNotSuperior = 0xc0190033,
        RecoveryNotNeeded = 0xc0190034,
        RmAlreadyStarted = 0xc0190035,
        FileIdentityNotPersistent = 0xc0190036,
        CantBreakTransactionalDependency = 0xc0190037,
        CantCrossRmBoundary = 0xc0190038,
        TxfDirNotEmpty = 0xc0190039,
        IndoubtTransactionsExist = 0xc019003a,
        TmVolatile = 0xc019003b,
        RollbackTimerExpired = 0xc019003c,
        TxfAttributeCorrupt = 0xc019003d,
        EfsNotAllowedInTransaction = 0xc019003e,
        TransactionalOpenNotAllowed = 0xc019003f,
        TransactedMappingUnsupportedRemote = 0xc0190040,
        TxfMetadataAlreadyPresent = 0xc0190041,
        TransactionScopeCallbacksNotSet = 0xc0190042,
        TransactionRequiredPromotion = 0xc0190043,
        CannotExecuteFileInTransaction = 0xc0190044,
        TransactionsNotFrozen = 0xc0190045,

        MaximumNtStatus = 0xffffffff
    }
    
    
public class ProcessInfo
{
    // Fields
    public int basePriority;
    public int handleCount;
    public int mainModuleId;
    public long pageFileBytes;
    public long pageFileBytesPeak;
    public long poolNonpagedBytes;
    public long poolPagedBytes;
    public long privateBytes;
    public int processId;
    public string processName;
    public int sessionId;
    public ArrayList threadInfoList = new ArrayList();
    public long virtualBytes;
    public long virtualBytesPeak;
    public long workingSet;
    public long workingSetPeak;
}


	        
public class PStatistics
{

[DllImport("ntdll.dll")]
public static extern NtStatus NtQueryInformationProcess([In] IntPtr ProcessHandle,
[In] ProcessInformationClass ProcessInformationClass,
out VmCounters ProcessInformation, [In] int ProcessInformationLength, [Optional] out int ReturnLength);
 
[DllImport("ntdll.dll")]
public static extern NtStatus NtQueryInformationProcess(
[In] IntPtr ProcessHandle, [In] ProcessInformationClass ProcessInformationClass, out int ProcessInformation, [In] int ProcessInformationLength, [Optional] out int ReturnLength);
 

 


 

[DllImport("ntdll.dll", CharSet = CharSet.Auto)]
public static extern int NtQuerySystemInformation(
int query, IntPtr dataPtr, int size, out int returnedSize);

    
    public enum ProcessInformationClass : int
    {
        ProcessBasicInformation, // 0
        ProcessQuotaLimits,
        ProcessIoCounters,
        ProcessVmCounters,
        ProcessTimes,
        ProcessBasePriority,
        ProcessRaisePriority,
        ProcessDebugPort,
        ProcessExceptionPort,
        ProcessAccessToken,
        ProcessLdtInformation, // 10
        ProcessLdtSize,
        ProcessDefaultHardErrorMode,
        ProcessIoPortHandlers,
        ProcessPooledUsageAndLimits,
        ProcessWorkingSetWatch,
        ProcessUserModeIOPL,
        ProcessEnableAlignmentFaultFixup,
        ProcessPriorityClass,
        ProcessWx86Information,
        ProcessHandleCount, // 20
        ProcessAffinityMask,
        ProcessPriorityBoost,
        ProcessDeviceMap,
        ProcessSessionInformation,
        ProcessForegroundInformation,
        ProcessWow64Information,
        ProcessImageFileName,
        ProcessLUIDDeviceMapsEnabled,
        ProcessBreakOnTermination,
        ProcessDebugObjectHandle, // 30
        ProcessDebugFlags,
        ProcessHandleTracing,
        ProcessIoPriority,
        ProcessExecuteFlags,
        ProcessResourceManagement,
        ProcessCookie,
        ProcessImageInformation,
        ProcessCycleTime,
        ProcessPagePriority,
        ProcessInstrumentationCallback, // 40
        ProcessThreadStackAllocation,
        ProcessWorkingSetWatchEx,
        ProcessImageFileNameWin32,
        ProcessImageFileMapping,
        ProcessAffinityUpdateMode,
        ProcessMemoryAllocationMode,
        ProcessGroupInformation,
        ProcessTokenVirtualizationEnabled,
        ProcessConsoleHostProcess,
        ProcessWindowInformation, // 50
        MaxProcessInfoClass
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VmCounters
    {
        public IntPtr PeakVirtualSize;
        public IntPtr VirtualSize;
        public int PageFaultCount;
        public IntPtr PeakWorkingSetSize;
        public IntPtr WorkingSetSize;
        public IntPtr QuotaPeakPagedPoolUsage;
        public IntPtr QuotaPagedPoolUsage;
        public IntPtr QuotaPeakNonPagedPoolUsage;
        public IntPtr QuotaNonPagedPoolUsage;
        public IntPtr PagefileUsage;
        public IntPtr PeakPagefileUsage;
    }
    
    [StructLayout(LayoutKind.Sequential)]
internal class SystemProcessInformation
{
    internal int NextEntryOffset;
    internal uint NumberOfThreads;
    private long SpareLi1;
    private long SpareLi2;
    private long SpareLi3;
    private long CreateTime;
    private long UserTime;
    private long KernelTime;
    internal ushort NameLength;
    internal ushort MaximumNameLength;
    internal IntPtr NamePtr;
    internal int BasePriority;
    internal IntPtr UniqueProcessId;
    internal IntPtr InheritedFromUniqueProcessId;
    internal uint HandleCount;
    internal uint SessionId;
    internal IntPtr PageDirectoryBase;
    internal IntPtr PeakVirtualSize;
    internal IntPtr VirtualSize;
    internal uint PageFaultCount;
    internal IntPtr PeakWorkingSetSize;
    internal IntPtr WorkingSetSize;
    internal IntPtr QuotaPeakPagedPoolUsage;
    internal IntPtr QuotaPagedPoolUsage;
    internal IntPtr QuotaPeakNonPagedPoolUsage;
    internal IntPtr QuotaNonPagedPoolUsage;
    internal IntPtr PagefileUsage;
    internal IntPtr PeakPagefileUsage;
    internal IntPtr PrivatePageCount;
    private long ReadOperationCount;
    private long WriteOperationCount;
    private long OtherOperationCount;
    private long ReadTransferCount;
    private long WriteTransferCount;
    private long OtherTransferCount;
}

 
[StructLayout(LayoutKind.Sequential)]
internal class SystemThreadInformation
{
    private long KernelTime;
    private long UserTime;
    private long CreateTime;
    private uint WaitTime;
    internal IntPtr StartAddress;
    internal IntPtr UniqueProcess;
    internal IntPtr UniqueThread;
    internal int Priority;
    internal int BasePriority;
    internal uint ContextSwitches;
    internal uint ThreadState;
    internal uint WaitReason;
}

public enum ThreadState
{
    Initialized,
    Ready,
    Running,
    Standby,
    Terminated,
    Wait,
    Transition,
    Unknown
}

public enum ThreadWaitReason
{
    Executive,
    FreePage,
    PageIn,
    SystemAllocation,
    ExecutionDelay,
    Suspended,
    UserRequest,
    EventPairHigh,
    EventPairLow,
    LpcReceive,
    LpcReply,
    VirtualMemory,
    PageOut,
    Unknown
}


 internal class ThreadInfo
{
    // Fields
    public int basePriority;
    public int currentPriority;
    public int processId;
    public IntPtr startAddress;
    public int threadId;
    public ThreadState threadState;
    public ThreadWaitReason threadWaitReason;
}

 

 internal static ThreadWaitReason GetThreadWaitReason(int value)
{
    switch (value)
    {
        case 0:
        case 7:
            return ThreadWaitReason.Executive;

        case 1:
        case 8:
            return ThreadWaitReason.FreePage;

        case 2:
        case 9:
            return ThreadWaitReason.PageIn;

        case 3:
        case 10:
            return ThreadWaitReason.SystemAllocation;

        case 4:
        case 11:
            return ThreadWaitReason.ExecutionDelay;

        case 5:
        case 12:
            return ThreadWaitReason.Suspended;

        case 6:
        case 13:
            return ThreadWaitReason.UserRequest;

        case 14:
            return ThreadWaitReason.EventPairHigh;

        case 15:
            return ThreadWaitReason.EventPairLow;

        case 0x10:
            return ThreadWaitReason.LpcReceive;

        case 0x11:
            return ThreadWaitReason.LpcReply;

        case 0x12:
            return ThreadWaitReason.VirtualMemory;

        case 0x13:
            return ThreadWaitReason.PageOut;
    }
    return ThreadWaitReason.Unknown;
}

 

    
public static VmCounters GetMemoryStatistics(IntPtr phandle)
{

 NtStatus status;
 VmCounters counters;
 int retLength;

 if ((status = NtQueryInformationProcess(
     phandle,
     ProcessInformationClass.ProcessVmCounters,
     out counters,
     Marshal.SizeOf(typeof(VmCounters)),
     out retLength
     )) >= NtStatus.Error)
     throw new ArgumentException(status.ToString());

     return counters;
    
}

   

private static int GetNewBufferSize(int existingBufferSize, int requiredSize)
{
    if (requiredSize != 0)
    {
        return (requiredSize + 0x2800);
    }
    int num = existingBufferSize * 2;
    if (num < existingBufferSize)
    {
        throw new OutOfMemoryException();
    }
    return num;
}

 

 

  public static ProcessInfo[] NtGetProcessInfos()
  {
  ProcessInfo[] processInfos;
  int size = 0x20000;
  int returnedSize = 0;
  GCHandle handle = new GCHandle();

   try
   {
        int num3;
        do
        {
            byte[] buffer = new byte[size];
            handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            num3 = NtQuerySystemInformation(5, handle.AddrOfPinnedObject(), size, out returnedSize);
            if (num3 == -1073741820)
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
                size = GetNewBufferSize(size, returnedSize);
            }
        }
        while (num3 == -1073741820);
        if (num3 < 0)
        {
        return null;
        }
   processInfos = NtGetProcessInfos(handle.AddrOfPinnedObject());
   return processInfos;

   }
    finally
    {
     if (handle.IsAllocated)
     {
     handle.Free();
     }
    }

  }

   public static ProcessInfo[] NtGetProcessInfos(IntPtr dataPtr)
  {
   IntPtr ptr;
   Hashtable hashtable = new Hashtable(100);
   uint num = 0;
  Label_000B:
   ptr = (IntPtr)(((uint)dataPtr) + num);
   SystemProcessInformation structure = new SystemProcessInformation();
   Marshal.PtrToStructure(ptr, structure);
   
   ProcessInfo info = new ProcessInfo();
    info.processId = structure.UniqueProcessId.ToInt32();
    info.handleCount = (int) structure.HandleCount;
    info.sessionId = (int) structure.SessionId;
    info.poolPagedBytes = (long) structure.QuotaPagedPoolUsage;
    info.poolNonpagedBytes = (long) structure.QuotaNonPagedPoolUsage;
    info.virtualBytes = (long) structure.VirtualSize;
    info.virtualBytesPeak = (long) structure.PeakVirtualSize;
    info.workingSetPeak = (long) structure.PeakWorkingSetSize;
    info.workingSet = (long) structure.WorkingSetSize;
    info.pageFileBytesPeak = (long) structure.PeakPagefileUsage;
    info.pageFileBytes = (long) structure.PagefileUsage;
    info.privateBytes = (long) structure.PrivatePageCount;
    info.basePriority = structure.BasePriority;

  
   hashtable[info.processId] = info;
   try
   {
    ptr = (IntPtr)(((long)ptr) + Marshal.SizeOf(structure));
   }
   catch
   {
   }
   for (int i = 0; i < structure.NumberOfThreads; i++)
   {
    SystemThreadInformation information2 = new SystemThreadInformation();
    Marshal.PtrToStructure(ptr, information2);
    ThreadInfo info2 = new ThreadInfo
    {
     processId = (int)information2.UniqueProcess,
     threadId = (int)information2.UniqueThread,
     basePriority = information2.BasePriority,
     currentPriority = information2.Priority,
     startAddress = information2.StartAddress,
     threadState = (ThreadState)information2.ThreadState,
     threadWaitReason = GetThreadWaitReason((int)information2.WaitReason)
    };
    info.threadInfoList.Add(info2);
    try
    {
     ptr = (IntPtr)(((long)ptr) + Marshal.SizeOf(information2));
    }
    catch
    {
    }
   }
   if (structure.NextEntryOffset != 0)
   {
   num += (uint)structure.NextEntryOffset;
   goto Label_000B;
   }
   
   ProcessInfo[] array = new ProcessInfo[hashtable.Values.Count];
   hashtable.Values.CopyTo(array, 0);
   return array;
  }

   public static bool IsNt()
{
    return (Environment.OSVersion.Platform == PlatformID.Win32NT);
}

 
public static ProcessInfo[] GetProcessInfos()
{
	if (IsNt())
    {
    return NtGetProcessInfos();
    }

    return OldGetProcessInfos();
}

[StructLayout(LayoutKind.Sequential)]
internal class WinProcessEntry
{
    public const int sizeofFileName = 260;
    public int dwSize;
    public int cntUsage;
    public int th32ProcessID;
    public IntPtr th32DefaultHeapID = IntPtr.Zero;
    public int th32ModuleID;
    public int cntThreads;
    public int th32ParentProcessID;
    public int pcPriClassBase;
    public int dwFlags;
}

[StructLayout(LayoutKind.Sequential)]
public class WinThreadEntry
{
    public int dwSize;
    public int cntUsage;
    public int th32ThreadID;
    public int th32OwnerProcessID;
    public int tpBasePri;
    public int tpDeltaPri;
    public int dwFlags;
}

 

 


[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern IntPtr CreateToolhelp32Snapshot(int flags, int processId);
  
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern bool Process32First(HandleRef handle, IntPtr entry);
 
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern bool Process32Next(HandleRef handle, IntPtr entry);
 
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern bool Thread32First(HandleRef handle, WinThreadEntry entry);
 
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern bool Thread32Next(HandleRef handle, WinThreadEntry entry);
 
[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
public static extern bool CloseHandle(HandleRef handle);
 

 
 public static ProcessInfo[] OldGetProcessInfos()
{
    IntPtr ptr = (IntPtr) (-1);
    GCHandle handle = new GCHandle();
    ArrayList list = new ArrayList();
    Hashtable hashtable = new Hashtable();
    try
    {
        ptr = CreateToolhelp32Snapshot(6, 0);
        if (ptr == ((IntPtr) (-1)))
        {
        return null;
        }
        int num = Marshal.SizeOf(typeof(WinProcessEntry));
        int val = num + 260;
        int[] numArray = new int[val / 4];
        handle = GCHandle.Alloc(numArray, GCHandleType.Pinned);
        IntPtr ptr2 = handle.AddrOfPinnedObject();
        Marshal.WriteInt32(ptr2, val);
        HandleRef ref2 = new HandleRef(null, ptr);
        if (Process32First(ref2, ptr2))
        {
            do
            {
                WinProcessEntry entry = new WinProcessEntry();
                Marshal.PtrToStructure(ptr2, entry);
                ProcessInfo info = new ProcessInfo();
                string path = Marshal.PtrToStringAnsi((IntPtr) (((long) ptr2) + num));
                info.processName = Path.ChangeExtension(Path.GetFileName(path), null);
                info.handleCount = entry.cntUsage;
                info.processId = entry.th32ProcessID;
                info.basePriority = entry.pcPriClassBase;
                info.mainModuleId = entry.th32ModuleID;
                hashtable.Add(info.processId, info);
                Marshal.WriteInt32(ptr2, val);
            }
            while (Process32Next(ref2, ptr2));
        }
        WinThreadEntry structure = new WinThreadEntry();
        structure.dwSize = Marshal.SizeOf(structure);
        if (Thread32First(ref2, structure))
        {
            do
            {
                ThreadInfo info2 = new ThreadInfo();
                info2.threadId = structure.th32ThreadID;
                info2.processId = structure.th32OwnerProcessID;
                info2.basePriority = structure.tpBasePri;
                info2.currentPriority = structure.tpBasePri + structure.tpDeltaPri;
                list.Add(info2);
            }
            while (Thread32Next(ref2, structure));
        }
        for (int i = 0; i < list.Count; i++)
        {
            ThreadInfo info3 = (ThreadInfo) list[i];
            ProcessInfo info4 = (ProcessInfo) hashtable[info3.processId];
            if (info4 != null)
            {
                info4.threadInfoList.Add(info3);
            }
        }
    }
    finally
    {
        if (handle.IsAllocated)
        {
            handle.Free();
        }
        if (ptr != ((IntPtr) (-1)))
        {
        CloseHandle(new HandleRef(null, ptr));
        }
    }
    ProcessInfo[] array = new ProcessInfo[hashtable.Values.Count];
    hashtable.Values.CopyTo(array, 0);
    return array;
}

 


 

 
}
}