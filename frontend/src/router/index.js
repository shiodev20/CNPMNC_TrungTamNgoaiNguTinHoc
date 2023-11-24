import store from "@/store";
import { createRouter, createWebHistory } from "vue-router";
const routes = [
  {
    path: "/",
    name: "home",
    component: import("../views/HomeView.vue"),
    beforeEnter(to, from, next) {
      store.dispatch("acount/get_all");
      const x = setInterval(() => {
        if (store.state.acount.emloyee != null) {
          console.log("Minh Khoi");
          clearInterval(x);
          store.state.acount.authencation =
            store.getters["acount/check_token"]();
          if (store.state.acount.authencation) {
            console.log(store.state.acount.authencation.role);
            if (store.state.acount.authencation.role == "user") {
              next();
            } else {
              next();
            }
          } else {
            next();
          }
        }
      }, 200);
    },
    children: [
      {
        path: "admin",
        name: "admin",
        beforeEnter(to, from, next) {
          store.dispatch("acount/get_all");
          const x = setInterval(() => {
            if (store.state.acount.emloyee != null) {
              clearInterval(x);
              store.state.acount.authencation =
                store.getters["acount/check_token"]();
              if (store.state.acount.authencation) {
                console.log(store.state.acount.authencation.role);
                if (store.state.acount.authencation.role == "user") {
                  next({ name: "user" });
                } else {
                  next();
                }
              } else {
                next();
              }
            }
          }, 200);
        },
        component: import("@/views/page/Main/AminVV.vue"),
        children: [
          {
            path: "exam",
            name: "exam",
            component: import("@/views/page/Admin/ExamV.vue"),
          },
          {
            path: "exambatch",
            name: "exambatch",
            component: import("@/views/page/Admin/ExamBatch.vue"),
          },
          {
            path: "branch",
            name: "branch",
            component: import("@/views/page/Admin/BranchV.vue"),
          },
          {
            path: "content_exam",
            name: "content_exam",
            component: import("@/views/page/Admin/ContentExamV.vue"),
          },
          {
            path: "languages",
            name: "languages",
            component: import("@/views/page/Admin/LanguagesV.vue"),
          },
          {
            path: "cerstificate",
            name: "cerstificate",
            component: import("@/views/page/Admin/CertificateV.vue"),
          },
          {
            path: "course",
            name: "course",
            component: import("@/views/page/Admin/CourseHMs.vue"),
          },
          {
            path: "class",
            name: "class",
            component: import("@/views/page/Admin/ClassV.vue"),
          },
          {
            path: "bill",
            name: "bill",
            component: import("@/views/page/Admin/BillV.vue"),
          },
        ],
      },
      {
        path: "/english",
        name: "english",
        beforeEnter(to, from, next) {
          store.dispatch("acount/get_all");
          const x = setInterval(() => {
            if (store.state.acount.emloyee != null) {
              clearInterval(x);
              store.state.acount.authencation =
                store.getters["acount/check_token"]();
              if (store.state.acount.authencation) {
                console.log(store.state.acount.authencation.role);
                if (store.state.acount.authencation.role == "user") {
                  next();
                } else {
                  next({ name: "admin" });
                }
              } else {
                next();
              }
            }
          }, 200);
        },
        component: import("@/views/page/Main/CustomerVV.vue"),
        children: [
          {
            path: "list-exam",
            name: "list-exam",
            component: import("@/views/page/Customer/WatchExam.vue"),
          },
          {
            path: "list-course",
            name: "list-course",
            component: import("@/views/page/Customer/WatchCourses.vue"),
          },
          {
            path: "list-myclass",
            name: "list-myclass",
            component: import("@/views/page/Customer/WatchMyclass.vue"),
          },
          {
            path: "list-myexam",
            name: "list-myexam",
            component: import("@/views/page/Customer/WatchMyexam.vue"),
          },
        ],
      },
    ],
  },
  {
    path: "/acount",
    name: "acount",
    component: import("@/views/page/Admin/AcountV.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
