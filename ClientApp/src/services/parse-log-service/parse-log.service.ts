import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CharacterInfo } from "../character-service/character-service.service";
import { API_URL } from 'src/app/app.api';

@Injectable({
  providedIn: 'root'
})
export class ParseLogService {


  constructor(private http: HttpClient, @Inject(API_URL) private baseUrl: string) {

  }

  getLogsForCharacter(id: number): Observable<LogParse[]> {
    return this.http.get<LogParse[]>(this.baseUrl + 'api/parse/character/' + id)
      .pipe(
        catchError(this.handleError<LogParse[]>())
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

export interface CharacterLogParse {
  jobClass: string;
  raidDPS: number;
  actualDPS: number;
  totalDamage: number;
  percentActive: number;
  hps: number;
  overhealPercent: number;
  damageTaken: number;
  character: CharacterInfo;
}

export interface LogParse {
  instanceName: string;
  timeTaken: number;
  succeeded: boolean;
  dateUploaded: Date;
  characterLogs: CharacterLogParse[];
}
