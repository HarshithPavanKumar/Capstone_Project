export interface Trainingprograms {
  id: number;
  coachId: number;
  name: string;
  duration: string; // e.g. "00:30:00"
  description: string;
  intensity: string;
  createdBy: string;
  createdDateTime: Date;
}
