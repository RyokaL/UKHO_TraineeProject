import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  getCharacterInfo(): Observable<CharacterInfo[]> {
    return this.http.get<CharacterInfo[]>(this.baseUrl + 'api/character')
      .pipe(
        catchError(this.handleError<CharacterInfo[]>())
        );
  }

  getCharacterById(id: number): Observable<CharacterInfo> {
    return this.http.get<CharacterInfo>(this.baseUrl + 'api/character/' + id)
      .pipe(
        catchError(this.handleError<CharacterInfo>())
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

export interface CharacterInfo {
  id: number;
  characterName: string;
  worldServer: string;
}
