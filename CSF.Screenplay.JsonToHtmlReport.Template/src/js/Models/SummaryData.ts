export interface SummaryData {
    features: ScreenplaySummary,
    scenarios: ScreenplaySummary,
}

export interface ScreenplaySummary {
    successCount: number,
    failCount: number,
    totalCount: number,
}