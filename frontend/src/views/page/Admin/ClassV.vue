<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
    <basedetail v-if="this.$route.query.action == 'detail'"></basedetail>
    <baselesson v-if="this.$route.query.action == 'detail-lesson'"></baselesson>
    <baseaddlession
      v-if="
        this.$route.query.action == 'create-lesson' ||
        this.$route.query.action == 'edit-lesson'
      "
    ></baseaddlession>
  </div>
</template>
<script>
import add from "@/components/class/Add_Class.vue";
import list from "@/components/class/List_Class.vue";
import detail from "@/components/class/Detail_Class.vue";
import lesson from "@/components/class/Lesson_Class.vue";
import addlesson from "@/components/class/Add_Session_class.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
    basedetail: detail,
    baselesson: lesson,
    baseaddlession: addlesson,
  },
};
</script>
<style scoped></style>
