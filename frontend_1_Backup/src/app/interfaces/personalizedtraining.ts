export interface Personalizedtraining {
  id: number;
  coachId: number;
  athleteId: number;
  description: string;
  duration: string;
  intensity: string;
  aiPredictionId: number;
  assignedDate: string; // ISO string, e.g. "2025-05-29T14:00:00Z"
  createdBy: string;
  createdDateTime: string;
}
