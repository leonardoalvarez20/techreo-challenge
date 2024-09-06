import { LoginRequest } from './login-request.model';

describe('CreateLoginRequest', () => {
  it('should create an instance', () => {
    expect(new LoginRequest()).toBeTruthy();
  });
});
