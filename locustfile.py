from locust import HttpUser, task, between

class MedFrontUser(HttpUser):
    wait_time = between(1, 2)

    def on_start(self):
        response = self.client.post(
            "/api/Auth/Login",
            json={
                "email": "load@test.com",
                "password": "LoadTest123!"
            }
        )

        if response.status_code == 200:
            data = response.json()
            self.token = data.get("accessToken")
        else:
            self.token = None

    @task
    def get_medications(self):
        if self.token:
            self.client.get(
                "/api/Medications",
                headers={
                    "Authorization": f"Bearer {self.token}"
                }
            )