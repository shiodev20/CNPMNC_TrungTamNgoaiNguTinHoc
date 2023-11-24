<template>
  <div v-if="data.id" class="list_exam">
    <h1>Lịch học</h1>
    <div class="Upperbar">
      <div class="upperbar_add">
        <div class="upperbar_item">
          <label>Tên lớp học</label>
          <input v-model="data_id.name" disabled type="text" />
        </div>
        <div class="upperbar_item">
          <label>Ngày bắt đầu</label>
          <input v-model="data_id.start_date" disabled type="date" />
        </div>
        <div class="upperbar_item">
          <label>Ngày kết thúc</label>
          <input v-model="data_id.end_date" disabled type="date" />
        </div>
        <div class="upperbar_item">
          <label>Địa điểm</label>
          <input v-model="branch_id.adress" disabled type="text" />
        </div>
      </div>
      <div
        v-if="
          this.$route.query.add == undefined &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="upperbar_button"
      >
        <button
          @click="
            this.$router.push({
              name: 'class',
              query: { action: 'create-lesson', id: this.$route.query.id },
            })
          "
        >
          Thêm tiết học
        </button>
      </div>
    </div>
    <div v-if="this.$route.query.add == undefined" class="list_exam_child">
      <!-- List exam -->
      <div class="list_exams">
        <table v-if="list_lesson.length > 0" class="table">
          <tr>
            <th>Mã</th>
            <th>Tiết học</th>
            <th>Giờ bắt đầu</th>
            <th>Giờ kết thúc</th>
            <th>Thời gian áp dụng</th>
            <th v-if="$store.state.acount.authencation.role == 'admin'">
              Chức năng
            </th>
          </tr>
          <tr :id="'item' + item.id" v-for="item in list_lesson" :key="item.id">
            <td>{{ item.id }}</td>
            <td>{{ item.shift }}</td>
            <td>{{ item.start_hour }}</td>
            <td>{{ item.end_hour }}</td>
            <td>{{ item.start_date + " -  " + item.end_date }}</td>
            <td
              v-if="$store.state.acount.authencation.role == 'admin'"
              class="setting"
            >
              <button
                @click="
                  this.$router.push({
                    name: 'class',
                    query: {
                      action: 'edit-lesson',
                      id: item.class_id,
                      idus: item.id,
                    },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_edit"
                  :icon="['fas', 'pen-fancy']"
                />
              </button>
              <button @click="deletes(item.id)">
                <font-awesome-icon
                  class="icon_trash"
                  :icon="['fas', 'trash-can']"
                />
              </button>
            </td>
          </tr>
        </table>
        <div class="images" v-else>
          <img src="../../Imagess/notfound.png" alt="" />
        </div>
      </div>
      <!-- end list exam -->
      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
</template>
<script>
import { computed, ref, watch } from "vue";
import store from "@/store";
export default {
  name: "HomeView",
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_lesson) {
      const div = document.getElementById("item" + this.list_lesson[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  created() {
    const check = setInterval(() => {
      if (store.state.class.class != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "detail"
            ? ((store.state.class.data_post.id = Number(this.$route.query.id)),
              (store.state.class.data_post.name = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].name),
              (store.state.class.data_post.course_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].course_id),
              (store.state.class.data_post.branch_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].branch_id),
              (store.state.class.data_post.start_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].start_date),
              (store.state.class.data_post.end_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].end_date))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    store.dispatch("class/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("lesson/get_all");

    const data = computed(() => {
      return store.state.class.data_post;
    });
    const data_id = computed(() => {
      return store.getters["class/getbyid"](data.value.id)[0];
    });
    const branch_id = computed(() => {
      return store.getters["branch/getbyid"](data_id.value.branch_id)[0];
    });
    const list_lesson = computed(() => {
      const x = store.state.lesson.lesson;
      console.log(
        x.filter((e) => e.class_id == store.state.class.data_post.id)
      );
      if (x == null) {
        return [];
      }
      return x.filter((e) => e.class_id == store.state.class.data_post.id);
    });
    const onsubmit = () => {
      const datas = [];
      document.querySelectorAll("#data").forEach((e) => {
        if (e.checked == true) {
          datas.push(Number(e.value));
          store.dispatch("customer/add_cus");
        }
      });
    };

    const this_page = ref(1);
    const limit_page = ref(3);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_lesson.value) {
        const div = document.getElementById(
          "item" + list_lesson.value[item].id
        );
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (list_lesson.value == null) {
        return 0;
      }
      return Math.ceil(list_lesson.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
    };

    const deletes = (id) => {
      store.dispatch("lesson/delete", id);
    };
    return {
      data,
      data_id,
      branch_id,
      list_lesson,
      onsubmit,
      count_page,
      chage,
      limit_page,
      this_page,
      run_app,
      deletes,
    };
  },
};
</script>
<style scoped>
.list_exam {
  width: 1000px;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  gap: 10px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
.btn {
  position: relative;
  width: 25px;
  display: block;
  height: 25px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
.group_check {
  position: relative;
}

.group_check {
  justify-content: center;
  display: flex;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
label {
  margin-bottom: 9px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.radio {
  display: none;
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}

.btnadd {
  border: none;
  border-radius: 10px;
  background: rgb(192, 2, 2);
  color: white;
  padding: 10px 40px;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  bottom: 10px;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
/* list exam  */
.select_table > label {
  width: 124px;
  height: 33px;
  flex-shrink: 0;
  border-radius: 10px 10px 0px 0px;
  border-bottom: none !important;
  border: 1px solid var(--color_main);
  color: rgb(67, 62, 62);
  position: relative;
}
.select_table > label > input {
  display: none;
}
.select_table > label > input:checked ~ .bg {
  background: #088395;
  color: white;
}
.bg {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: center;
  border-radius: 10px 10px 0px 0px;
  align-items: center;
  border-bottom: none !important;
  cursor: pointer;
  background: #0e6d7b;
}
.select_table {
  position: absolute;
  display: flex;
  top: -33px;
}
table {
  border-collapse: collapse;
  position: relative;
  width: 100%;
}
.tale {
  margin-left: auto;
  margin-right: auto;
}

th,
td {
  padding: 8px;
  text-align: center;
  vertical-align: middle !important;
}

tr:nth-child(even) {
  background-color: var(--color_row);
}

th {
  background-color: var(--color_table);
  color: white;
}

.setting > button {
  border-radius: 20px;
  background: white;
  border: none;
  margin-left: 10px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.icon_trash,
.icon_edit,
.icon_detail,
.icon_clipboad {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon_edit {
  color: rgb(85, 250, 85);
}

/* end list exam  */
/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 180px;
  margin: 10px;
  position: relative;
}
.dropdown_input {
  position: relative;
  padding: 20px 20px;
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  pointer-events: none;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */

h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* upperbar */
.Upperbar {
  display: flex;
  height: 72px;
  padding: 5px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
  position: relative;
}
.upperbar_button > button {
  width: 146px;
  height: 33px;
  border-radius: 10px;
  background: var(--color_main);
  color: white;
  font-size: 13px;
}
.upperbar_button {
  position: absolute;
  right: 10px;
  bottom: 20px;
}
.upperbar_add {
  display: flex;
  justify-content: start;
  gap: 10px;
}
.upperbar_add > div {
  display: flex;
  flex-direction: column;
  align-items: start;
}
.upperbar_add > div > input {
  height: 33px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}
.upperbar_add > div > label {
  margin-bottom: 5px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
/* end upperbar */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  padding-bottom: 67px;
  position: relative;
  padding-top: 50px;
  margin-top: 10px;
  min-height: 400px;
}
/* end list parent  */
</style>
