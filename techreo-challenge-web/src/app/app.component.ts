import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>', // Usa el RouterOutlet para cargar componentes según las rutas
  standalone: true,
  imports: [RouterOutlet] // Asegúrate de importar RouterOutlet
})
export class AppComponent {
}
