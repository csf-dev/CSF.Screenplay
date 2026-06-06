/**
 * This type and those which are used in its object graph should be kept in-sync with the C# types of the same names.
 * Those are found in the `CSF.Screenplay` project, in the directory `ReportModel`.
 */
export interface ScreenplayReport {
    Metadata : ReportMetadata,
    Performances : PerformanceReport[]
}

export interface ReportMetadata {
    Timestamp : string,
    ReportVersion : string,
}

export interface PerformanceReport {
    NamingHierarchy : IdentifierAndNameModel[],
    Outcome : string,
    Reportables : ReportableModelBase[],
    Started : string
}

export interface IdentifierAndNameModel {
    Id : string | null,
    Name : string,
    IsGeneratedId : boolean
}

export const
    actorCreatedReportKind = 'ActorCreatedReport',
    actorGainedAbilityReportKind = 'ActorGainedAbilityReport',
    actorSpotlitReportKind = 'ActorSpotlitReport',
    spotlightTurnedOffReportKind = 'SpotlightTurnedOffReport',
    performableReportKind = 'PerformableReport';

interface ReportableModelBase {
    Report : string,
    Actor : string,
    Started : string,
}

export interface ActorCreatedReport extends ReportableModelBase {
    Kind : typeof actorCreatedReportKind
}

export interface ActorGainedAbilityReport extends ReportableModelBase {
    Kind : typeof actorGainedAbilityReportKind
}

export interface ActorSpotlitReport extends ReportableModelBase {
    Kind : typeof actorSpotlitReportKind
}

export interface SpotlightTurnedOffReport extends ReportableModelBase {
    Kind : typeof spotlightTurnedOffReportKind
}

export interface PerformableReport extends ReportableModelBase {
    Kind : typeof performableReportKind,
    Type : string,
    Phase : string,
    Ended : string,
    Result : string,
    HasResult : boolean,
    Exception : string,
    ExceptionIsBubbling : boolean,
    Assets : PerformableAsset[],
    Reportables: ReportableModel[]
}

export interface PerformableAsset {
    ContentType : string,
    Path : string,
    Data : string,
    Name : string,
    Summary : string
}

export type ReportableModel =
    | ActorCreatedReport
    | ActorGainedAbilityReport
    | ActorSpotlitReport
    | SpotlightTurnedOffReport
    | PerformableReport;
    
export type ReportableKind = ReportableModel['Kind'];
