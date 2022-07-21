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
        catchError((err, _) => {
          console.log(err)
          return this.handleError<CharacterInfo[]>()
        })
      );
  }

  getCharactersByUserId(): Observable<CharacterInfo[]> {
    return this.http.get<CharacterInfo[]>(this.apiUrl + 'api/character/user')
    .pipe(
      catchError((err, _) => this.handleError<CharacterInfo[]>())
    );
  }

  getCharacterById(id: number): Observable<CharacterInfo> {
    return this.http.get<CharacterInfo>(this.apiUrl + 'api/character/' + id)
      .pipe(
        catchError((err, _) => this.handleError<CharacterInfo>())
      );
  }

  addUserToCharacter(character: CharacterInfoReq): boolean {
    this.http.put<any>(this.apiUrl + 'api/character/user', { characterName: character.characterName, worldServer: character.worldServer }, {observe: 'response'})
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
      // Let the app keep running by returning an empty result.
      return of(result as T);
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