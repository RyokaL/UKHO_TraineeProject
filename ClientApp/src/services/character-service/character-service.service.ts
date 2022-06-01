import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { API_URL } from 'src/app/app.api';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  
  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) {

  }

  getCharacterInfo(): Observable<CharacterInfo[]> {
    return this.http.get<CharacterInfo[]>(this.apiUrl + 'api/character')
      .pipe(
        catchError(this.handleError<CharacterInfo[]>())
        );
  }

  getCharactersByUserId(userId: string): Observable<CharacterInfo[]> {
    return this.http.get<CharacterInfo[]>(this.apiUrl + 'api/character/user/' + userId)
    .pipe(
      catchError(this.handleError<CharacterInfo[]>())
    );
  }

  getCharacterById(id: number): Observable<CharacterInfo> {
    return this.http.get<CharacterInfo>(this.apiUrl + 'api/character/' + id)
      .pipe(
        catchError(this.handleError<CharacterInfo>())
      );
  }

  addUserToCharacter(character: CharacterInfoReq, userId: string): boolean {
    this.http.put<any>(this.apiUrl + 'api/character', { characterName: character.characterName, worldServer: character.worldServer, userId: userId }, {observe: 'response'})
      .subscribe(response => {
        return response.ok
      });
    return false;
  }

  updatePrivacy(character: CharacterInfoReq, privacy: boolean): boolean {
    this.http.put<any>(this.apiUrl + 'api/character/user/private', { characterName: character.characterName, worldServer: character.worldServer, private: privacy }, {observe: 'response'})
      .subscribe(response => {
        return response.ok
      });
    return false;
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
  private: boolean;
}

export interface CharacterInfoReq {
  characterName: string,
  worldServer: string
}