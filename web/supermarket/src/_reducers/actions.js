export const userPostFetch = user => {
    return dispatch => {
      return fetch("https://localhost:5001/api/users", {
        method: "POST",
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
        body: JSON.stringify(user)
      })
        .then(resp => resp.json())
        .then(data => { 
          if (data.message) {
            // Here you should have logic to handle invalid creation of a user.
            // This assumes your Rails API will return a JSON object with a key of
            // 'message' if there is an error with creating the user, i.e. invalid username
            window.alert("oops!!");
          } else {
            window.alert("success!!");
            console.log(data);
            console.log(JSON.stringify(user));

          }
        })
        .catch(err => {
          console.log("Something went wrong");
          console.log(err);
        })
    }
  }
  
  export const userLoginFetch = user => {
    return dispatch => {
      return fetch("https://localhost:5001/api/login", {
        method: "POST",
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
        body: JSON.stringify({user})
      })
        .then(resp => resp.json())
        .then(data => {
          if (data.message) {
            // Here you should have logic to handle invalid login credentials.
            // This assumes your Rails API will return a JSON object with a key of
            // 'message' if there is an error
          } else {
            localStorage.setItem("token", data.AccessToken)
            dispatch(loginUser(data.user))
          }
        })
    }
  }

  const loginUser = userObj => ({
    type: 'LOGIN_USER',
    payload: userObj
})