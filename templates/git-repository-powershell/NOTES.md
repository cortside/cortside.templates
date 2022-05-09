https://docs.atlassian.com/bitbucket-server/rest/7.6.0/bitbucket-rest.html#idp291

/rest/api/1.0/projects/{projectKey}/repos/{repositorySlug}/pull-requests

This API can also be invoked via a user-centric URL when addressing repositories in personal projects.

Create a new pull request between two branches. The branches may be in the same repository, or different ones. When using different repositories, they must still be in the same {@link Repository#getHierarchyId() hierarchy}.

The authenticated user must have REPO_READ permission for the "from" and "to"repositories to call this resource.

Example request representations:

application/json [collapse]

{
    "title": "Talking Nerdy",
    "description": "It’s a kludge, but put the tuple from the database in the cache.",
    "state": "OPEN",
    "open": true,
    "closed": false,
    "fromRef": {
        "id": "refs/heads/feature-ABC-123",
        "repository": {
            "slug": "my-repo",
            "name": null,
            "project": {
                "key": "PRJ"
            }
        }
    },
    "toRef": {
        "id": "refs/heads/master",
        "repository": {
            "slug": "my-repo",
            "name": null,
            "project": {
                "key": "PRJ"
            }
        }
    },
    "locked": false,
    "reviewers": [
        {
            "user": {
                "name": "charlie"
            }
        }
    ]
}


{
    "id": 101,
    "version": 1,
    "title": "Talking Nerdy",
    "description": "It’s a kludge, but put the tuple from the database in the cache.",
    "state": "OPEN",
    "open": true,
    "closed": false,
    "createdDate": 1359075920,
    "updatedDate": 1359075920,
    "fromRef": {
        "id": "refs/heads/feature",
        "displayId": "feature-ABC-123",
        "latestCommit": "babecafebabecafebabecafebabecafebabecafe",
        "repository": {
            "slug": "my-repo",
            "id": 1,
            "name": "My repo",
            "description": "My repo description",
            "hierarchyId": "e3c939f9ef4a7fae272e",
            "scmId": "git",
            "state": "AVAILABLE",
            "statusMessage": "Available",
            "forkable": true,
            "project": {
                "key": "PRJ",
                "id": 1,
                "name": "My Cool Project",
                "description": "The description for my cool project.",
                "public": true,
                "type": "NORMAL",
                "links": {
                    "self": [
                        {
                            "href": "http://link/to/project"
                        }
                    ]
                }
            },
            "public": true,
            "links": {
                "clone": [
                    {
                        "href": "ssh://git@<baseURL>/PRJ/my-repo.git",
                        "name": "ssh"
                    },
                    {
                        "href": "https://<baseURL>/scm/PRJ/my-repo.git",
                        "name": "http"
                    }
                ],
                "self": [
                    {
                        "href": "http://link/to/repository"
                    }
                ]
            }
        }
    },
    "toRef": {
        "id": "refs/heads/master",
        "displayId": "master",
        "latestCommit": "cafebabecafebabecafebabecafebabecafebabe",
        "repository": {
            "slug": "my-repo",
            "id": 1,
            "name": "My repo",
            "description": "My repo description",
            "hierarchyId": "e3c939f9ef4a7fae272e",
            "scmId": "git",
            "state": "AVAILABLE",
            "statusMessage": "Available",
            "forkable": true,
            "project": {
                "key": "PRJ",
                "id": 1,
                "name": "My Cool Project",
                "description": "The description for my cool project.",
                "public": true,
                "type": "NORMAL",
                "links": {
                    "self": [
                        {
                            "href": "http://link/to/project"
                        }
                    ]
                }
            },
            "public": true,
            "links": {
                "clone": [
                    {
                        "href": "ssh://git@<baseURL>/PRJ/my-repo.git",
                        "name": "ssh"
                    },
                    {
                        "href": "https://<baseURL>/scm/PRJ/my-repo.git",
                        "name": "http"
                    }
                ],
                "self": [
                    {
                        "href": "http://link/to/repository"
                    }
                ]
            }
        }
    },
    "locked": false,
    "author": {
        "user": {
            "name": "alice",
            "emailAddress": "alice@example.com",
            "id": 92903040,
            "displayName": "Alice",
            "active": true,
            "slug": "alice",
            "type": "NORMAL"
        },
        "role": "PARTICIPANT",
        "approved": false,
        "status": "UNAPPROVED"
    },
    "reviewers": [
        {
            "user": {
                "name": "jcitizen",
                "emailAddress": "jane@example.com",
                "id": 101,
                "displayName": "Jane Citizen",
                "active": true,
                "slug": "jcitizen",
                "type": "NORMAL"
            },
            "lastReviewedCommit": "7549846524f8aed2bd1c0249993ae1bf9d3c9998",
            "role": "REVIEWER",
            "approved": false,
            "status": "UNAPPROVED"
        }
    ],
    "participants": [],
    "links": {
        "self": [
            {
                "href": "http://link/to/pullrequest"
            }
        ]
    }
}