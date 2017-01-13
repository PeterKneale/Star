- Operations
  - CreateLogin
    - Request - `{ UserId, UserName, Password }`
    - Reply - `{ }`
  - DeleteLogin
    - Request - `{ UserId }`
    - Reply - `{ }`
  - PerformLogin
    - Request - `{ UserName, Password }`
    - Reply - `{ LoginResult, UserId }`
    
- Models
  - LoginResult `SUCCESS|FAILED`
    
- Events
 - LoginEvent - `{ LoginResult, UserId }`
 - LoginCreatedEvent - `{ UserId }`
 - LoginDeletedEvent - `{ UserId }`
